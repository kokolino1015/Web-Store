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
        public CategoryController(CategoryService _categoryService, CommonService _commonService) : base(_commonService)
        {
            categoryService = _categoryService;
            commonService = _commonService;
        }

        public IActionResult Index()
        {
            return View();
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
           
            categoryService.Create(model);
            return RedirectToAction("Index", "Home");
            
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(categoryService.GetCategoryById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(CategoryFormModel model)
        {
            categoryService.Update(model);
            return RedirectToAction("Index", "Home");
            
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(categoryService.GetCategoryById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(CategoryFormModel model)
        {
            categoryService.Delete(model.Id);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = categoryService.GetCategoryById(id);
            ViewBag.Products = categoryService.GetAllIdByCategoryId(id);

            return View(model);
        }
        [HttpGet]
        public IActionResult All()
        {
            var categories = categoryService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();

        }
    }
}
