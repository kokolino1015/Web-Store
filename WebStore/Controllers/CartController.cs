using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using WebStore.Data.Entities;
using WebStore.Data.Entities.Account;
using WebStore.Models;
using WebStore.Models.ProductModel;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        private readonly CartService cartService;
        private readonly CommonService commonService;
        private readonly ChargeService chargeService;
        private static Dictionary<int, PaymentModel> models = new Dictionary<int, PaymentModel>();
        public CartController(CartService _cartService, CommonService _commonService, ChargeService _chargeService)
        {
            chargeService = _chargeService;
            cartService = _cartService;
            commonService = _commonService;
        }
        
        public ActionResult Details(int id)
        {
            CartViewModel model = cartService.GetCartById(id);

            return View(model);
        }
        public ActionResult Add(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            cartService.Add(id, user.Cart.Id);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("/Cart/Order/{id}")]
        public IActionResult Order(int id)
        {
            Payment payment = cartService.MakePayment(id);
            PaymentModel paymentModel = cartService.MakePaymentModel(payment);
            models[paymentModel.Id] = paymentModel;
            return View(paymentModel);
        }

        [HttpPost("/Cart/Order/{id}")]
        public IActionResult Order(int id, string stripeToken, string stripeEmail)
        {

            Dictionary<string,string> Metadata = cartService.GetPaymentById(id);
            var options = new ChargeCreateOptions
            {
                Amount = (long)(models[id].Amount * 100),
                Currency = "USD",
                Description = models[id].Description,
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
                Metadata = Metadata
            };

            var charge = this.chargeService.Create(options);

            return Redirect("/");
        }
    }
}
