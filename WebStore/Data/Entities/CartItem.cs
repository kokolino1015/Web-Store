namespace WebStore.Data.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product product;
        public int Quantity;
    }
}
