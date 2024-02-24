namespace WebStore.Data.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product product { get; set; }
        public int Quantity { get; set; }
    }
}
