using WebStore.Data;
using WebStore.Data.Entities.Account;
using WebStore.Data.Entities;
using WebStore.Models.CategoryModel;

namespace WebStore.Services
{
    public class CategoryService
    {
        private readonly ApplicationDbContext context;

        public CategoryService(ApplicationDbContext _context)
        {
            this.context = _context;
        }
        public void Create(CategoryFormModel model)
        {
            Category category = new Category()
            {
                Name = model.Name,
                IsDeleted = false 
            };
            context.Categories.Add(category);
            context.SaveChanges();
        }
        public void Update(CategoryFormModel model)
        {
            Category category = context.Categories.Find(model.Id);
            category.Name = model.Name;
            this.context.SaveChanges();
        }

        public CategoryFormModel GetCategoryById(int id)
        {
            return context.Categories.Where(x => x.Id == id).Select(Category => new CategoryFormModel
            {
                Id = id,
                Name = Category.Name,
            }).FirstOrDefault();
        }
        public void Delete(int id)
        {
            var model = this.context.Categories.Find(id);
            model.IsDeleted = true;
            this.context.SaveChanges();
        }
        public List<Product> GetAllIdByCategoryId(int id)
        {
            return context
                .Products
                .Where(Product => Product.Category.Id == id && !Product.isDeleted)
                .ToList();
        }
        public List<Category> GetAllCategories()
        {
            return context.Categories.Where(x => !x.IsDeleted).ToList();
        }
    }
}
