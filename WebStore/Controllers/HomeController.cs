using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebStore.Data.Entities.Account;
using WebStore.Models;
using WebStore.Models.ProductModel;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryService categoryService;
        private readonly CommonService commonService;
        private readonly ProductService productService;
        public HomeController(ILogger<HomeController> logger, CategoryService categoryService, CommonService commonService, ProductService productService):base(commonService)
        {
            _logger = logger;
            this.categoryService = categoryService;
            this.commonService = commonService;
            this.productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            
            List<ProductFormModel> products = await productService.GetFirst10Ads();
            ViewBag.Products = products;
            var categories = categoryService.GetAllCategories();
            ViewBag.Categories = categories;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}