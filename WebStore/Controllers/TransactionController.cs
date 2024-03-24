using Microsoft.AspNetCore.Mvc;
using Stripe;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ChargeService chargeService;
        private readonly CommonService commonService;
        public TransactionController(ChargeService chargeService, CommonService _commonService) : base(_commonService)
        {
            commonService = _commonService;
            this.chargeService = chargeService;
        }

        public IActionResult Index()
        {
            return View(this.chargeService.List().ToList());
        }
    }
}
