using WebStore.Data.Entities;

namespace WebStore.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}
