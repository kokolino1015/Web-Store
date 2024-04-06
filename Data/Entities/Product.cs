namespace WebStore.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public Category Category { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool isDeleted { get; set; }
    }
}
