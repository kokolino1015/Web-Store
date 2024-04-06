using Microsoft.AspNetCore.Mvc;
using Stripe;
using WebStore.Models;
using WebStore.Models.SalesModel;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class SalesController : BaseController
    {
        private readonly CommonService commonService;
        private readonly SalesService salesService;
        
        public SalesController(CommonService _commonService, SalesService _salesService) : base(_commonService)
        {        
            commonService = _commonService;
            salesService = _salesService;
        }
        [HttpGet]
        public IActionResult SaleOnProduct()
        {
            ViewBag.Products = salesService.GetProducts();
            SalesOnProduct model = new SalesOnProduct();
            return View(model);
        }
        [HttpPost]
        public IActionResult SaleOnProduct(SalesOnProduct model)
        {
            salesService.SetSaleForProduct(model);
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public IActionResult SaleOnCategory()
        {
            ViewBag.Categories = salesService.GetCategories();
            SalesOnCategory model = new SalesOnCategory();
            return View(model);
        }
        [HttpPost]
        public IActionResult SaleOnCategory(SalesOnCategory model)
        {
            salesService.SetSaleForCategory(model);
            return RedirectToAction("Index", "Home");
        }
    }
}
