using WebStore.Data.Entities.Account;
using WebStore.Data;
using WebStore.Models.ReviewModel;
using WebStore.Data.Entities;

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
            Review review = context.Reviews.Where(x => x.Id == id).FirstOrDefault();
            ReviewViewModel bookViewModel = new ReviewViewModel()
            {
                Description = review.Description
            };
            return bookViewModel;
        }
        public void Edit(ReviewViewModel model)
        {
            Review review = context.Reviews.Where(x => x.Id == model.Id).FirstOrDefault();
            review.Description = model.Description;
            context.Reviews.Update(review);
            context.SaveChanges();
        }
        public void Delete(ReviewViewModel model)
        {
            Review review = context.Reviews.Where(x=> x.Id == model.Id).FirstOrDefault();
            review.isDeleted = true;
            context.Reviews.Update(review);
            context.SaveChanges();
        }
    }
}
