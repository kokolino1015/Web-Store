using WebStore.Data.Entities.Account;
using WebStore.Data;
using WebStore.Models.ReviewModel;
using WebStore.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace WebStore.Services
{
    public class ReviewService
    {
        private readonly ApplicationDbContext context;
        public ReviewService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public ReviewViewModel GetModelForCreate(int id)
        {
            ReviewViewModel model = new ReviewViewModel()
            {
                Product = id
            };
            return model;
        }
        public void Add(ReviewViewModel model, ApplicationUser user)
        {
            Product product = context.Products.Where(x=> x.Id == model.Product).FirstOrDefault();
            Review review = new Review()
            {
                Description = model.Description,
                Owner = user.Id,
                Product = product,
            };
            context.Reviews.Add(review);
            context.SaveChanges();
        }
        public ReviewViewModel GetModelForEditAndDelete(int id)
        {
            Review review = context.Reviews.Where(x => x.Id == id).Include(x=> x.Product).FirstOrDefault();
            ReviewViewModel reviewModel = new ReviewViewModel()
            {
                Description = review.Description,
                Product = review.Product.Id
            };
            return reviewModel;
        }
        public int Edit(ReviewViewModel model)
        {
            Review review = context.Reviews.Where(x => x.Id == model.Id).Include(x=> x.Product).FirstOrDefault();
            review.Description = model.Description;
            context.Reviews.Update(review);
            context.SaveChanges();
            return review.Product.Id;
        }
        public int Delete(ReviewViewModel model)
        {
            Review review = context.Reviews.Where(x => x.Id == model.Id).Include(x => x.Product).FirstOrDefault();
            review.isDeleted = true;
            context.Reviews.Update(review);
            context.SaveChanges();
            return review.Product.Id;
        }
    }
}
