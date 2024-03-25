using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data.Entities.Account;
using WebStore.Models.CategoryModel;
using WebStore.Data.Entities;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class CategoryController : Controller
    {

        private readonly CategoryService categoryService;
        private readonly CommonService commonService;
        public CategoryController(CategoryService _categoryService, CommonService _commonService)
        {
            categoryService = _categoryService;
            commonService = _commonService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            //if (commonService.FindRole(User).Name != "employer")
            //{
            //    return Unauthorized();
            //}
            CategoryFormModel model = new CategoryFormModel();
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(CategoryFormModel model)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            //if (commonService.FindRole(User).Name != "employer")
            //{
            //    return Unauthorized();
            //}
            categoryService.Create(model);
            //return RedirectToAction("Index", "Home");
            //return RedirectToAction("Create", "Create");

            return View("Edit", model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int catId)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            return View(categoryService.GetCategoryById(catId));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(CategoryFormModel model)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            categoryService.Update(model);
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("ListCategories", "category");
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(int catId)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            //return View(categoryService.GetCategoryById(catId));
            
            categoryService.Delete(catId);
            //return View("ListCategories", ListCategories());
            return RedirectToAction("ListCategories");
        }

        /*
        [Authorize]
        [HttpPost]
        public IActionResult Delete(CategoryFormModel model)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            categoryService.Delete(model.Id);
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("All", "category");
        }
        */

        [HttpGet]
        public IActionResult Details(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            ViewBag.Owner = user;
            var model = categoryService.GetCategoryById(id);
            ViewBag.Products = categoryService.GetAllIdByCategoryId(id);

            return View(model);
        }
        [HttpGet]
        public IActionResult ListCategories()
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            //var user = commonService.FindUser(User);
            //ViewBag.Owner = null;
            //if (user != null)
            //{
            //    ViewBag.Role = commonService.FindRole(User).Name;
            //    ViewBag.Owner = user;
            //}

            List<Category> categories = categoryService.GetAllCategories();

            List<CategoryFormModel> allCategories = categories
                .Select(cat => new CategoryFormModel { Id = cat.Id, Name = cat.Name })
                .OrderBy(cat => cat.Id)
                .ToList();

            //ViewBag.Categories = categories;
            //return View();

            return View(allCategories);

        }
    }
}
