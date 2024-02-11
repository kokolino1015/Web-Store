using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebStore.Data.Entities.Account;
using WebStore.Models;
using WebStore.Models.ProductModel;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryService categoryService;
        private readonly CommonService commonService;
        private readonly ProductService productService;
        public HomeController(ILogger<HomeController> logger, CategoryService categoryService, CommonService commonService, ProductService productService)
        {
            _logger = logger;
            this.categoryService = categoryService;
            this.commonService = commonService;
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = commonService.FindUser(User);
            if (user != null)
            {
                ViewBag.CartId = user.Cart.Id;
            }
            List<ProductFormModel> products = await productService.GetFirst10Ads();
            ViewBag.Products = products;
            var categories = categoryService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();
        }

        public IActionResult Privacy()
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}