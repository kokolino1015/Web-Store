using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data.Entities.Account;
using WebStore.Models.CategoryModel;
using WebStore.Models.ProductModel;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService productService;
        private readonly CommonService commonService;
        private readonly CategoryService categoryService;
        public ProductController(ProductService _productService, CommonService _commonService, CategoryService _categoryService)
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
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            //if (commonService.FindRole(User).Name != "employer")
            //{
            //    return Unauthorized();
            //}
            ViewBag.Categories = productService.GetCategories();
            ProductFormModel model = new ProductFormModel();
            return View(model);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(ProductFormModel model)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            //if (commonService.FindRole(User).Name != "employer")
            //{
            //    return Unauthorized();
            //}
            
            productService.Create(model);
            //return RedirectToAction("Index", "Home");
            return RedirectToAction("Index", "Admin");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            ViewBag.Categories = categoryService.GetAllCategories();
            return View(productService.GetProductById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(ProductFormModel model)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            productService.Update(model);
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("All", "category");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            return View(productService.GetProductById(id));
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(CategoryFormModel model)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            productService.Delete(model.Id);
            return RedirectToAction("Index", "Home");
            //return RedirectToAction("All", "category");
        }
        [HttpGet("/Product/Details/{id}")]
        public IActionResult Details(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            ViewBag.Owner = user;
            var model = productService.GetProductById(id);
            return View(model);
        }
    }
}
