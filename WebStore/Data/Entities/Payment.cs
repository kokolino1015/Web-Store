namespace WebStore.Data.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int AmountOfItems { get; set; }
        public Cart Cart { get; set; }
        public List<string> Items { get; set; }
    }
}
