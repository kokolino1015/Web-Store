using WebStore.Data.Entities;

namespace WebStore.Models.SalesModel
{
    public class SalesOnCategory
    {
        public string Title { get; set; }
        public Category Category { get; set; }
        public decimal Discount { get; set; }
    }
}
