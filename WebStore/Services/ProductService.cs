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
                Quantity = model.Quantity,
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
        public ProductViewModel GetProductById(int id)
        {
            return context.Products.Where(x => x.Id == id).Select(x => new ProductViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Description = x.Description,
                Photos = x.Photos,
                Reviews = context.Reviews.Where(y => y.Product.Id == x.Id && !y.isDeleted).Select(y => new Review
                {
                    Id = y.Id,
                    Description = y.Description,
                    Owner = y.Owner
                }).ToList(),
                Category = x.Category.Name,
                Quantity = x.Quantity,
            }).FirstOrDefault();
        }
        public ProductFormModel GetAdById(int id)
        {
            return context.Products.Where(x => x.Id == id).Select(product => new ProductFormModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.Category.Id,
                Price = product.Price,
                Quantity = product.Quantity,
            }).FirstOrDefault();
        }

        public void Update(ProductFormModel model)
        {
            Product ad = context.Products.Find(model.Id);
            ad.Description = model.Description;
            ad.Name = model.Name;
            ad.Price = model.Price;
            ad.Quantity = model.Quantity;
            ad.Category = context.Categories.Where(x => x.Id == model.Category).FirstOrDefault();
            this.context.SaveChanges();
        }
        public void Delete(int id)
        {
            var model = this.context.Products.Find(id);
            model.isDeleted = true;

            // Delete photos and reviews for product
            List<Photo> photos = context.Photos.Where(p => p.ProductId == id).ToList();
            context.Photos.RemoveRange(photos);

            List<Review> reviews = context.Reviews.Where(r => r.Product.Id == id).ToList();
            context.Reviews.RemoveRange(reviews);

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
                Quantity = x.Quantity,
            }).ToListAsync();
        }

        public List<ProductFormModel> GetProductByName(string productName)
        {
            List<ProductFormModel> products = context.Products
                //.Where(p => p.Name == productName && !p.isDeleted)
                .Where(p => !p.isDeleted && p.Name.Contains(productName))
                .Select(x => new ProductFormModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Category = x.Category.Id,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    //Reviews = x.Reviews,
                    Reviews = context.Reviews.Where(r => !r.isDeleted && r.Product.Id == x.Id).ToList(),
                    Photos = context.Photos.Where(p => p.ProductId == x.Id).ToList(),
                }).ToList(); 
            return products;
        }

        public List<ProductFormModel> GetProdsByCat(int catId)
        {
            List<ProductFormModel> products = context.Products
                //.Where(p => p.Name == productName && !p.isDeleted)
                .Where(p => !p.isDeleted && p.Category.Id == catId)
                .Select(x => new ProductFormModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Category = x.Category.Id,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    //Reviews = x.Reviews,
                    Reviews = context.Reviews.Where(r => !r.isDeleted && r.Product.Id == x.Id).ToList(),
                    Photos = context.Photos.Where(p => p.ProductId == x.Id).ToList(),
                }).ToList();
            return products;
        }
    }
}
