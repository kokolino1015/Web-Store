using WebStore.Data.Entities.Account;

namespace WebStore.Data.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ApplicationUser Owner { get; set; }
        public Product Product { get; set; }
        public bool isDeleted { get; set; }
    }
}
