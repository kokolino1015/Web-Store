namespace WebStore.Data.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; } = new List<Product>();
    }
}
