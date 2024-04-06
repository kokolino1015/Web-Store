using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Data.Entities.Account;

namespace WebStore.Data.Entities
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Description { get; set; }
        public string Owner { get; set; }
        public Product Product { get; set; }
        public bool isDeleted { get; set; }
    }
}
