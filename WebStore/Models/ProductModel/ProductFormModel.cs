using WebStore.Data.Entities;

namespace WebStore.Models.ProductModel
{
    public class ProductFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public int Category { get; set; }
        public decimal Price { get; set; }
    }
}
