using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data.Entities.Account;
using WebStore.Models.CategoryModel;
using WebStore.Models.ProductModel;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class ProductController : BaseController
    {
        private readonly ProductService productService;
        private readonly CommonService commonService;
        private readonly CategoryService categoryService;
        public ProductController(ProductService _productService, CommonService _commonService, CategoryService _categoryService) : base(_commonService)
        {
            categoryService = _categoryService;
            productService = _productService;
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
            ViewBag.Categories = productService.GetCategories();
            ProductFormModel model = new ProductFormModel();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(ProductFormModel model)
        {
            productService.Create(model);
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Categories = categoryService.GetAllCategories();
            return View(productService.GetProductById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(ProductFormModel model)
        {
            productService.Update(model);
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("All", "category");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(productService.GetProductById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(CategoryFormModel model)
        {
            productService.Delete(model.Id);
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("All", "category");
        }
        [HttpGet("/Product/Details/{id}")]
        public IActionResult Details(int id)
        {
            var model = productService.GetProductDetails(id);
            return View(model);
        }
    }
}
