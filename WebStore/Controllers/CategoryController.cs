using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data.Entities.Account;
using WebStore.Models.CategoryModel;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class CategoryController : BaseController
    {

        private readonly CategoryService categoryService;
        private readonly CommonService commonService;
        public CategoryController(CategoryService _categoryService, CommonService _commonService):base(commonService:_commonService)
        {
            categoryService = _categoryService;
            commonService = _commonService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
          
            CategoryFormModel model = new CategoryFormModel();
            return View(model);
        }
        [Authorize]
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
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            return View(categoryService.GetCategoryById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(CategoryFormModel model)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            categoryService.Update(model);
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("All", "category");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            return View(categoryService.GetCategoryById(id));
        }
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
        public IActionResult All()
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
            var categories = categoryService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();

        }
    }
}
