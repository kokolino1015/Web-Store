

using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data.Entities;
using WebStore.Data.Entities.Account;
using WebStore.Models.CategoryModel;
using WebStore.Models.ProductModel;
using WebStore.Services;

namespace WebStore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductService productService;
        private readonly CommonService commonService;
        private readonly CategoryService categoryService;
        public ProductController(ProductService _productService, CommonService _commonService, CategoryService _categoryService)
        {
            categoryService = _categoryService;
            productService = _productService;
            commonService = _commonService;
        }

        public IActionResult Index()
        {
            List<Category> categories = categoryService.GetAllCategories();

            List<CategoryFormModel> allCategories = categories
                .Select(cat => new CategoryFormModel { Id = cat.Id, Name = cat.Name })
                .OrderBy(cat => cat.Id)
                .ToList();

            //ViewData["Categories"] = allCategories;
            //return View();

            return View(allCategories);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            //if (commonService.FindRole(User).Name != "employer")
            //{
            //    return Unauthorized();
            //}
            ViewBag.Categories = productService.GetCategories();
            //ProductFormModel model = new ProductFormModel();
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(ProductFormModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = commonService.FindUser(User);
                ViewBag.CartId = user.Cart.Id;
                //if (commonService.FindRole(User).Name != "employer")
                //{
                //    return Unauthorized();
                //}
                productService.Create(model);
                //return RedirectToAction("Index", "Home");

                TempData["successMessage"] = "Product created successfully";
                int catId = model.Category;
                //return RedirectToAction("Index", "Product");
                return ListProdByCat(catId);
                //return View("ListProdByCat", model.Id);
            }
            else
            {
                TempData["errorMessage"] = "Model data is not valid!";
                ViewBag.Categories = productService.GetCategories();
                return View();
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            ViewBag.Categories = categoryService.GetAllCategories();
            return View(productService.GetAdById(id));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(ProductFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ApplicationUser user = commonService.FindUser(User);
                    ViewBag.CartId = user.Cart.Id;
                    productService.Update(model);

                    //return RedirectToAction("Index", "Home");
                    //return RedirectToAction("All", "category");

                    TempData["successMessage"] = "Product updated successfully";
                    //int catId = model.Category;
                    //return RedirectToAction("Index", "Product");
                    return ListProdByCat(model.Category);
                    //return View("ListProdByCat", model.Id);
                }
                else
                {
                    TempData["errorMessage"] = "Model data is not valid!";
                    ViewBag.Categories = productService.GetCategories();
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            ViewData["Categories"] = productService.GetCategories();
            return View(productService.GetAdById(id));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(ProductFormModel model)
        {
            try
            {
                ApplicationUser user = commonService.FindUser(User);
                ViewBag.CartId = user.Cart.Id;
                productService.Delete(model.Id);

                TempData["successMessage"] = "Product deleted successfully!";
                return ListProdByCat(model.Category);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        //return Redirect($"/Product/Details/{model.Product}");
        [HttpGet("/Product/Details/{id}")]
        public IActionResult Details(int id)
        {
            ApplicationUser user = commonService.FindUser(User);
            ViewBag.CartId = user.Cart.Id;
            ViewBag.Owner = user;
            var model = productService.GetProductById(id);
            return View(model);
        }


        //[HttpPost]
        //public IActionResult ListProdByCat(CategoryFormModel category)

        [HttpGet]
        //[HttpPost]
        public IActionResult ListProdByCat(int id)
        {
            List<ProductFormModel> products = productService.GetProdsByCat(id);

            ViewData["CategoryId"] = id;
            ViewData["CategoryName"] = categoryService.GetCategoryById(id).Name;
            //ViewData["ProductsList"] = products;

            return View("ListProdByCat", products);
        }

        [HttpPost]
        public async Task<IActionResult> ListProdByName(string productName)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                TempData["errorMessage"] = "Enter product name!";
                return RedirectToAction("Index");
            }

            ViewData["CategoriesList"] = productService.GetCategories();
            var products = productService.GetProductByName(productName);
            return View("ListProdByName", products);
        }
    }
}
