using Microsoft.AspNetCore.Mvc;
using WebStore.Services;
using Microsoft.AspNetCore.Authorization;

using WebStore.Data.Entities;
using WebStore.Models.ProductModel;
using WebStore.Models.PhotoModel;
//using Stripe;

namespace WebStore.Controllers
{
    public class PhotoController : Controller
    {
        private readonly PhotoService photoService;
        private readonly ProductService productService;
        
        public PhotoController(PhotoService _photoService, ProductService _productService)
        {
            photoService = _photoService;
            productService = _productService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UploadPhotos(int id)
        {
            //ViewBag.Categories = categoryService.GetAllCategories();

            //ProductFormModel
            ViewData["Product"] = productService.GetAdById(id);
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult UploadPhotos(int id, PhotoUploadModel model)
        {
            //ViewBag.Categories = categoryService.GetAllCategories();
            photoService.Upload(id, model);

            ProductFormModel product = productService.GetAdById(id);
            //return View("ListProdByCat", products);
            return Redirect($"/Photo/ListProductPhotos/{product.Id}");
        }

        public IActionResult ListProductPhotos(int id)
        {
            // id is product id
            List<PhotoViewModel> photos = photoService.ListProductPhotos(id);
            ViewData["Product"] = productService.GetAdById(id);
            ViewData["CategoriesList"] = productService.GetCategories();
            return View("ListProductPhotos", photos);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(photoService.GetPhotoById(id));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(PhotoViewModel model)
        {
            try
            {
                
                photoService.Delete(model.Id);


                TempData["successMessage"] = "Photo deleted successfully!";
                return ListProductPhotos(model.ProductId);
                
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
    }


}
