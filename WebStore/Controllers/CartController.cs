using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public CartController(CartService _cartService, CommonService _commonService)
        {
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

        // GET: CartController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CartController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CartController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CartController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CartController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
