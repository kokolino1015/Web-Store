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
    public class CartController : BaseController
    {
        private readonly CartService cartService;
        private readonly CommonService commonService;
        private readonly ChargeService chargeService;
        private readonly IEmailService emailService;
        private static Dictionary<int, PaymentModel> models = new Dictionary<int, PaymentModel>();
        public CartController(CartService _cartService, CommonService _commonService, ChargeService _chargeService, IEmailService emailService):base(_commonService)
        {
            chargeService = _chargeService;
            cartService = _cartService;
            commonService = _commonService;
            this.emailService = emailService;
        }
        
        public ActionResult Details(int id)
        {
            CartViewModel model = cartService.GetCartById(id);

            return View(model);
        }
        public ActionResult Add(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
           
            cartService.Add(id, user.Cart.Id);
            return Redirect($"/Cart/Details/{user.Cart.Id}");
        }
        public ActionResult Remove(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            
            cartService.Remove(id, user.Cart.Id);
            return Redirect($"/Cart/Details/{user.Cart.Id}");
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
            ApplicationUser user = commonService.FindUser(User);
            cartService.ClearCart(id);
            Payment payment = cartService.GetPayment(id);
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
            List<Stripe.Charge> transactions = this.chargeService.List().ToList();
            string content = $"{@charge.Created} - "+ "**** **** **** "
                + $"{charge.PaymentMethodDetails.Card.Last4}) - {string.Format("{0:F2}$", (charge.Amount / 100.0M))})\n"
                + $"{String.Join(',', payment.Items)} - {payment.Amount}";
            var message = new Message((new string[] { user.Email! }).ToList(), "Receipt", content);
            emailService.SendEmail(message);
            return Redirect("/");
        }
        
    }
}
