using System.Drawing;

namespace WebStore.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public Category Category { get; set; }

        //Navigation property: configure one-to-many relationship with Photo
        public List<Photo> Photos { get; set; } = new List<Photo>();
        public decimal Price { get; set; }
        public int  Quantity { get; set; }

        public bool isDeleted { get; set; }
    }
}
