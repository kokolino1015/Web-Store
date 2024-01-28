using WebStore.Data;
using WebStore.Models;
using WebStore.Models.CategoryModel;

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
            return context.Carts.Where(x => x.Id == id).Select(cart => new CartViewModel
            {
                Id = id,
                Products = cart.Products,
            }).FirstOrDefault();
        }
    }
}
