using Microsoft.AspNetCore.Mvc;
using Stripe;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ChargeService chargeService;
        private readonly CommonService commonService;
        public TransactionController(ChargeService chargeService, CommonService commonService):base(commonService)
        {
            this.chargeService = chargeService;
            this.commonService = commonService;
        }

        public IActionResult Index()
        {
            return View(this.chargeService.List().ToList());
        }
    }
}
