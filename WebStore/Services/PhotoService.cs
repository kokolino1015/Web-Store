using System.Reflection.Metadata;
using WebStore.Data;
using WebStore.Data.Entities;
using WebStore.Models.PhotoModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WebStore.Models.ReviewModel;

namespace WebStore.Services
{
    public class PhotoService
    {
        private readonly ApplicationDbContext context;

        public PhotoService(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public void Upload(int id, PhotoUploadModel photos)
        {
            foreach (var file in photos.MultiplePhotos)
            {
                if (file.Length > 0)
                {
                    //Creating a unique File Name
                    var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);

                    //Save the uploaded files to the database and the File System in the code below.
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", uniqueFileName);

                    // Using Streaming
                    using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    {
                        //This will save to Local folder
                        file.CopyTo(stream);
                    }
                    // To do 
                    // Delete file from wwwroot\upload when photo is deleted from database

                    // Create an instance of Photo
                    var photo = new Photo()
                    {
                        Name = uniqueFileName,
                        FileExtension = Path.GetExtension(file.FileName),
                        Size = file.Length,
                        // Convert file to array of bytes
                        //Bytes = ConvertToByteArray(filePath),
                        Bytes = File.ReadAllBytes(filePath),
                        ProductId = id
                    };

                    // Save to database
                    context.Photos.Add(photo);
                    context.SaveChanges();
                }

            }
        }

        public List<PhotoViewModel> ListProductPhotos(int id) 
        {
            // id is product id
            List<PhotoViewModel> photos = context.Photos
                .Where(p => p.ProductId == id)
                .Select(x => new PhotoViewModel()
                {
                    Id = x.Id,
                    Bytes = x.Bytes,
                    Name = x.Name,
                    FileExtension= x.FileExtension,
                    Size = x.Size,
                    ProductId = id,
                })
                .ToList();
            return photos;
        }

        public PhotoViewModel GetPhotoById(int id)
        {
            Photo photo = context.Photos.Find(id);
            PhotoViewModel photoViewModel = new PhotoViewModel()
            {
                Id = photo.Id,
                Bytes = photo.Bytes,
                Name = photo.Name,
                FileExtension = photo.FileExtension,
                Size = photo.Size,
                ProductId = photo.ProductId,
            };

            return photoViewModel;
        }

        public void Delete(int id)
        {
            Photo photo = context.Photos.Find(id);
            context.Photos.Remove(photo);
            context.SaveChanges();

            // To do 
            // Delete file from wwwroot\upload when photo is deleted from database
        }
    }
}
