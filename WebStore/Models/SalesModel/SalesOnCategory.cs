using WebStore.Data.Entities;

namespace WebStore.Models.SalesModel
{
    public class SalesOnCategory
    {
        public string Title { get; set; }
        public int Category { get; set; }
        public decimal Discount { get; set; }
    }
}
