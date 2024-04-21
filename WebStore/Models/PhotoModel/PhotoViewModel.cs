namespace WebStore.Models.PhotoModel
{
    public class PhotoViewModel
    {
        public int Id { get; set; }
        public byte[] Bytes { get; set; }
        public string Name { get; set; }
        public string FileExtension { get; set; }
        public decimal Size { get; set; }
        public int ProductId { get; set; }
    }
}
