using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebStore.Data.Entities.Account;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class BaseController : Controller
    {
        public readonly CommonService commonService;
        public BaseController(CommonService _commonService)
        {

            commonService = _commonService;

        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            ApplicationUser user = commonService.FindUser(User);
            if (user != null)
            {
                ViewBag.CartId = user.Cart.Id;
            }
        }
    }
}
