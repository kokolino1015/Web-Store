using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Data.Entities
{
    public class Photo
    {
        [Key]
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
