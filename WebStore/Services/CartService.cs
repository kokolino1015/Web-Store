using Microsoft.EntityFrameworkCore;
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
            var model = context.Carts.Where(x => x.Id == id).Include(c=> c.Items).Select(cart => new CartViewModel
            {
                Id = id,
                Items = cart.Items,
            }).FirstOrDefault();

            foreach (var element in model.Items)
            {
                var product = context.CartItems.Where(x => x.Id == element.Id).Include(x=> x.product).FirstOrDefault();
                var item = model.Items.Where(x => x.Id == element.Id).FirstOrDefault();
                item.product = product.product;
            };
            return model;
        }
        public void Add(int product, int id)
        {
            Cart cart =  context.Carts.Where(x => x.Id == id).Include(x => x.Items).First();
            Product _product = context.Products.Where(x => x.Id == product).First();
            var items = cart.Items;
            if (items.Where(x=> x.product == _product).Count() == 0)
            {
                CartItem cartItem = new CartItem
                {
                    product = _product,
                    Quantity = 1
                };
                cart.Items.Add(cartItem);
                context.CartItems.Add(cartItem);
            }
            else
            {
                var item = items.Where(x => x.product.Id == _product.Id).First();
                item.Quantity += 1;
            }            
            context.Carts.Update(cart);
            context.SaveChanges();
        }

        //public void Remove(ProductFormModel product, int id)
        //{
        //    Cart cart = context.Carts.Where(x => x.Id == id).First();
        //    Product _product = context.Products.Where(x => x.Id == product.Id).First();
        //    cart.Items.Remove();
        //    context.Carts.Update(cart);
        //}
    }
}
