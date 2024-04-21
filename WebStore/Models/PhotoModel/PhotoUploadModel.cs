namespace WebStore.Models.PhotoModel
{
    public class PhotoUploadModel
    {
        public List<IFormFile> MultiplePhotos { get; set; }
        
        //public IFormCollection MultiplePhotos { get; set; }
    }
}
