using WebStore.Data;
using WebStore.Data.Entities;
using WebStore.Models.SalesModel;

namespace WebStore.Services
{
    public class SalesService
    {
        private readonly ApplicationDbContext context;
        public SalesService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public List<Product> GetProducts()
        {
            
            return context.Products.ToList();
        }
        public void SetSaleForProduct(SalesOnProduct model)
        {
            Product product = context.Products.Where(x=> model.Product.Id == x.Id).FirstOrDefault();
            product.DiscountPrice = (1-(model.Discount/100)) * product.OriginalPrice;
            context.Products.Update(product);
            context.SaveChanges();
        }
        public List<Category> GetCategories()
        {
            return context.Categories.ToList();
        }
        public void SetSaleForCategory(SalesOnCategory model)
        {
            List<Product> products = context.Products.Where(x => model.Category.Id == x.Category.Id).ToList();
            foreach (Product product in products)
            {
                product.DiscountPrice = (1 - (model.Discount / 100)) * product.OriginalPrice;
                context.Products.Update(product);
                context.SaveChanges();
            }
        }


    }
}
