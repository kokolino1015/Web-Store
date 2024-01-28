using WebStore.Data.Entities;

namespace WebStore.Models.ProductModel
{
    public class ProductFormModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
    }
}
