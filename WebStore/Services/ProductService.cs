using WebStore.Data.Entities.Account;
using WebStore.Data.Entities;
using WebStore.Data;
using WebStore.Models.ProductModel;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Security.Claims;
using WebStore.Models.ReviewModel;

namespace WebStore.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext context;

        public ProductService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public void Create(ProductFormModel model)
        {
            Product product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Category = context.Categories.Where(x => x.Id == model.Category).FirstOrDefault(),
                isDeleted = false
            };
            context.Products.Add(product);
            context.SaveChanges();
        }
        public List<Category> GetCategories()
        {
            return context.Categories.Where(x => !x.IsDeleted).Select(x => new Category
            {
                Id = x.Id,
                Name = x.Name,
            }).ToList();
        }
        public ProductFormModel GetProductById(int id)
        {
            return context.Products.Where(x => x.Id == id).Select(x => new ProductFormModel
            {
                Id = x.Id,
                Name= x.Name, 
                Price = x.Price, 
                Description = x.Description,
                Reviews = context.Reviews.Where(y => y.Product.Id == x.Id && !y.isDeleted).Select(y=> new Review
                {
                    Id = y.Id,
                    Description = y.Description,
                    Owner = y.Owner
                }).ToList(),
                Category = x.Category.Id
            }).FirstOrDefault();
        }
        public ProductViewModel GetProductDetails(int id)
        {
            return context.Products.Where(x => x.Id == id).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                Reviews = context.Reviews.Where(y => y.Product.Id == x.Id && !y.isDeleted).Select(y => new Review
                {
                    Id = y.Id,
                    Description = y.Description,
                    Owner = y.Owner
                }).ToList(),
                Category = x.Category.Name
            }).FirstOrDefault();
        }
        
        public void Update(ProductFormModel model)
        {
            Product ad = context.Products.Find(model.Id);
            ad.Description = model.Description;
            ad.Name = model.Name;
            ad.Price = model.Price;
            ad.Category = context.Categories.Where(x => x.Id == model.Category).FirstOrDefault();
            this.context.SaveChanges();
        }
        public void Delete(int id)
        {
            var model = this.context.Products.Find(id);
            model.isDeleted = true;
            this.context.SaveChanges();
        }
        
        public async Task<List<ProductFormModel>> GetFirst10Ads()
        {
            return await context.Products.Take(10).Where(x => !x.isDeleted).Select(x => new ProductFormModel()
            {
                Id = x.Id,
                Category = x.Category.Id,
                Description = x.Description, 
                Name = x.Name,
                Price = x.Price,
            }).ToListAsync();
        }
    }
}
