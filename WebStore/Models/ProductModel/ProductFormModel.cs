using System.ComponentModel.DataAnnotations;
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

        [Range(0, 999999.99)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Quantity { get; set; }

        public List<Photo> Photos { get; set; } = new List<Photo>(); 
    }
}
