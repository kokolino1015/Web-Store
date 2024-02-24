using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public Payment MakePayment(int id)
        {
            Cart cart = context.Carts.Where(x=> x.Id == id).Include(x=> x.Items).First();
            foreach (var element in cart.Items)
            {
                var product = context.CartItems.Where(x => x.Id == element.Id).Include(x => x.product).FirstOrDefault();
                var item = cart.Items.Where(x => x.Id == element.Id).FirstOrDefault();
                item.product = product.product;
            };
            decimal amount = 0;
            int amountOfItems = 0;
            List<string> productNames = new List<string>();
            foreach (var item in cart.Items)
            {
                amountOfItems += item.Quantity;
                amount += item.Quantity * item.product.Price;
                productNames.Add(item.product.Name);
            }
            Payment payment = new Payment()
            {
                AmountOfItems = amountOfItems,
                Amount = amount,
                Items = productNames,
                Cart = cart
            };
            context.Payments.Add(payment);
            context.SaveChanges();
            return payment;
        }
        public Dictionary<string,string> GetPaymentById(int id)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            Payment payment = context.Payments.Where(x => x.Id == id).FirstOrDefault();
            string products = String.Join(", ", payment.Items);
            result[products] = payment.AmountOfItems.ToString();
            return result;
        }
        public PaymentModel MakePaymentModel(Payment payment)
        {
            return new PaymentModel()
            {
                Id = payment.Id,
                ProductName = String.Join(", ", payment.Items),
                Amount = payment.Amount,
                Company = "Some company",
                Description = $"{String.Join(", ", payment.Items)} for {payment.Amount}$",
                Label = $"Pay {payment.Amount}$"
            };
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
