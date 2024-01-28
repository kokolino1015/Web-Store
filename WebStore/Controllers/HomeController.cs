using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebStore.Data.Entities.Account;
using WebStore.Models;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CategoryService categoryService;
        private readonly CommonService commonService;
        public HomeController(ILogger<HomeController> logger, CategoryService categoryService, CommonService commonService)
        {
            _logger = logger;
            this.categoryService = categoryService;
            this.commonService = commonService;
        }

        public IActionResult Index()
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
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