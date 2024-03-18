using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data.Entities.Account;
using WebStore.Data.Entities;
using WebStore.Models.ProductModel;
using WebStore.Services;
using Microsoft.EntityFrameworkCore;
using WebStore.Models.RoleModel;
using WebStore.Models.AccountModel;


namespace WebStore.Controllers
{
    //Accessible only to user with admin role
    [Authorize(Roles = "Admin")]
	public class AdminController : Controller
	{
        private readonly UserManager<ApplicationUser> userManager;
		private readonly RoleManager<IdentityRole> roleManager;
        private readonly ProductService productService;
		private readonly CommonService commonService;
		private readonly CategoryService categoryService;
		public AdminController(
				UserManager<ApplicationUser> _userManager,
				RoleManager<IdentityRole> _roleManager,
				ProductService _productService, 
				CommonService _commonService, 
				CategoryService _categoryService)
		{
			userManager = _userManager;
			roleManager = _roleManager;
			categoryService = _categoryService;
			productService = _productService;
			commonService = _commonService;
		}

		public IActionResult Index()
		{
			return View();
		}

        // Render a view
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        // Add the new role into database
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel roleModel)
        {
            if (ModelState.IsValid)
            {
                // Check if the role already exists
                bool roleExists = await roleManager.RoleExistsAsync(roleModel?.RoleName);
                if (roleExists)
                {
                    ModelState.AddModelError("", "Role Already Exists");
                }
                else
                {
                    // Create the role
                    // We just need to specify a unique role name to create a new role
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = roleModel?.RoleName
                    };
                    // Saves the role in the underlying AspNetRoles table
                    IdentityResult result = await roleManager.CreateAsync(identityRole);
                    if (result.Succeeded)
                    {
                        //return RedirectToAction("Index", "Home");
                        return View("Index");
                    }
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(roleModel);
        }

        // Fetch all roles
        [HttpGet]
        public async Task<IActionResult> ListRoles()
        {
            List<IdentityRole> roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }

        // Find role by id
        [HttpGet]
        public async Task<IActionResult> EditRole(string roleId)
        {
            //First Get the role information from the database
            IdentityRole role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                // Handle the scenario when the role is not found
                return View("Error");
            }
            //Populate the EditRoleViewModel from the data retrived from the database
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            return View(model);
        }

        // Find role by name
        [HttpPost]
        public async Task<IActionResult> FindRoleByName(string roleName)
        {
            //First Get the role information from the database
            IdentityRole role = await roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return View("Error");
            }
            //Populate the EditRoleViewModel from the data retrived from the database
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await roleManager.FindByIdAsync(model.Id);
                if (role == null)
                {
                    // Handle the scenario when the role is not found
                    ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                    return View("NotFound");
                }
                else
                {
                    role.Name = model.RoleName;

                    var result = await roleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ListRoles"); // Redirect to the roles list
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);
            if (role == null)
            {
                // Role not found, handle accordingly
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var result = await roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                // Role deletion successful
                return RedirectToAction("ListRoles"); // Redirect to the roles list page
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            // If we reach here, something went wrong, return to the view
            return View("ListRoles", await roleManager.Roles.ToListAsync());
        }

        public IActionResult AssignRole()
        {
            return View();
        }

        // Find user by email
        [HttpPost]
        public async Task<IActionResult> FindUserByEmail(AccountViewModel accountModel)
        {
            //First Get the user information from the database
            var user = await userManager.FindByNameAsync(accountModel.Username) ;
            var userRoles = await userManager.GetRolesAsync(user); //Return list of rolenames
            if (user == null)
            {
                return View("Error");
            }

            var allRoles = await roleManager.Roles.ToListAsync();
            
            //Populate the SetRoleViewModel from the data retrived from the database
            SetRoleViewModel model = new SetRoleViewModel
            {
                //Id = user.Id,
                UserName = user.Email.ToString(),
                RoleName = userRoles.FirstOrDefault("None"),
                AllRoles = allRoles
            };

            //ViewBag.AllRoles = allRoles;

            //ViewBag.UserRoles = roles;
           
            return View(model);
        }

        public async Task<IActionResult> SetUserRole(string userName, string roleName)
        {
            ApplicationUser user = await userManager.FindByNameAsync(userName);

            // Check if the user is in the role already
            var isInRole = await userManager.IsInRoleAsync(user, roleName);
            if (isInRole)
            {
                ModelState.AddModelError("", "User is already in role");
                //return RedirectToAction("FindUserByEmail");
            }

            var userRoles = await userManager.GetRolesAsync(user);
            if (userRoles.Any())
            {
                var currentRole = userRoles.FirstOrDefault();
                await userManager.RemoveFromRoleAsync(user, currentRole);
            }

            var result = await userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                var allRoles = await roleManager.Roles.ToListAsync();
                // Remove current role of user from list of roles
                IdentityRole role = allRoles.Find(x => x.Name == roleName);
                allRoles.Remove(role);

                var model = new SetRoleViewModel
                {
                    UserName = user.Email,
                    RoleName = roleName,
                    AllRoles = allRoles
            };

                return View("FindUserByEmail", model);
            }
            else
                return View("Error");
        }

        public IActionResult Edit(ProductFormModel model)
		{
			productService.Update(model);
			return View("Index");
		}

		public IActionResult Delete(int id)
		{
			productService.Delete(id);
			return View("Index");
			//return DeleteConfirm
		}
	}
}
