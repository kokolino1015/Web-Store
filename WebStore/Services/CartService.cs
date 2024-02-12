using WebStore.Data;
using WebStore.Data.Entities;
using WebStore.Models;
using WebStore.Models.CategoryModel;
using WebStore.Models.ProductModel;

namespace WebStore.Services
{
    public class CartService
    {
        private readonly ApplicationDbContext context;

        public CartService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public CartViewModel GetCartById(int id)
        {
            var model =  context.Carts.Where(x => x.Id == id).Select(cart => new CartViewModel
            {
                Id = id,
                Products = cart.Products,
            }).FirstOrDefault();
            return model;
        }
        public void Add(int product, int id)
        {
            Cart cart = context.Carts.Where(x => x.Id == id).First();
            Product _product = context.Products.Where(x => x.Id == product).First();
            cart.Products.Add(_product);
            context.Carts.Update(cart);
            context.SaveChanges();
        }
        public void Remove(ProductFormModel product, int id)
        {
            Cart cart = context.Carts.Where(x => x.Id == id).First();
            Product _product = context.Products.Where(x => x.Id == product.Id).First();
            cart.Products.Remove(_product);
            context.Carts.Update(cart);
        }
    }
}
