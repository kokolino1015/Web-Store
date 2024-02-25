using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebStore.Data.Entities.Account;
using WebStore.Data;
using WebStore.Services;
using WebStore.Models.ReviewModel;
using WebStore.Data.Entities;

namespace WebStore.Controllers
{
    public class ReviewController : Controller
    {
        private readonly CommonService commonService;
        private readonly ReviewService reviewService;
        public ReviewController(CommonService commonService, ReviewService reviewService)
        {
            
            this.commonService = commonService;
            this.reviewService = reviewService;
        }
        [Authorize]
        [HttpGet]
        public IActionResult Create(int id)
        {
            ReviewViewModel model = reviewService.GetModelForCreate(id);
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(ReviewViewModel model)
        {
            ApplicationUser user = commonService.FindUser(User);
            reviewService.Add(model, user);
            return Redirect($"/Product/Details/{model.Product}");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(reviewService.GetModelForEditAndDelete(id));
        }
        [HttpPost]
        public IActionResult Edit(ReviewViewModel model)
        {
            int id = reviewService.Edit(model);
            return Redirect($"/Product/Details/{id}");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            
            return View(reviewService.GetModelForEditAndDelete(id));
        }
        [HttpPost]
        public IActionResult Delete(ReviewViewModel model)
        {
            int id = reviewService.Delete(model);
            return Redirect($"/Product/Details/{id}");
        }
    }
}
