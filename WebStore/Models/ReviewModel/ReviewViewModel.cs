using WebStore.Data.Entities.Account;
using WebStore.Data.Entities;

namespace WebStore.Models.ReviewModel
{
    public class ReviewViewModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public ApplicationUser Owner { get; set; }
        public int Product { get; set; }
    }
}
