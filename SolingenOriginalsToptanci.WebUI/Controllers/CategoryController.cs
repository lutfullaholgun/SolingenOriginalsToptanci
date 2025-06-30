using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data;

namespace SolingenOriginalsToptanci.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly SolingenContext _context;

        public CategoryController(SolingenContext context)
        {
            _context = context;
        }

        // /Category/meyve-bicaklari
        public IActionResult Index(string slug)
        {
            if (string.IsNullOrEmpty(slug))
                return NotFound();

            var category = _context.Categories
                .Include(c => c.SubCategories)
                .FirstOrDefault(c => c.Slug == slug);

            if (category == null)
                return NotFound();

            return View(category);
        }

        // /Category/meyve-bicaklari/1-grup
        public IActionResult SubCategory(string categorySlug, string subCategorySlug)
        {
            if (string.IsNullOrEmpty(categorySlug) || string.IsNullOrEmpty(subCategorySlug))
                return NotFound();

            var subCategory = _context.SubCategories
                .Include(sc => sc.Category)
                .FirstOrDefault(sc => sc.Slug == subCategorySlug && sc.Category.Slug == categorySlug);

            if (subCategory == null)
                return NotFound();

            // Ürünleri çekmek istersen burada yapabilirsin
            // var products = _context.Products.Where(p => p.SubCategoryId == subCategory.Id).ToList();

            return View(subCategory);
        }
    }
}
