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
    public class ReviewController : BaseController
    {
        private readonly CommonService commonService;
        private readonly ReviewService reviewService;
        private readonly ProductService productService;
        public ReviewController(CommonService commonService, ReviewService reviewService, ProductService productService):base(commonService)
        {
            
            this.commonService = commonService;
            this.reviewService = reviewService;
            this.productService = productService;
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

        [HttpGet("/Review/ListProductReviews/{id}")]
        public IActionResult ListProductReviews(int id)
        {
            List<ReviewViewModel> reviews = reviewService.GetProductReviews(id);
            ViewData["ProductName"] = productService.GetAdById(id).Name;
            ViewData["CategoriesList"] = productService.GetCategories();
            return View(reviews);
        }
    }
}
