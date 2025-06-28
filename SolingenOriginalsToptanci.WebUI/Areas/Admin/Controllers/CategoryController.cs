using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.Data;
using SolingenOriginalsToptanci.Models.Entities;

namespace SolingenOriginalsToptanci.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly SolingenContext _context;

        public CategoryController(SolingenContext context)
        {
            _context = context;
        }

        // Kategori listesi
        public IActionResult Index()
        {
            var categories = _context.Categories.ToList();
            return View(categories);
        }

        // Yeni kategori formu
        public IActionResult Create()
        {
            return View();
        }

        // Yeni kategori kaydet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // Düzenleme formu
        public IActionResult Edit(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // Düzenlemeyi kaydet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        // Silme işlemi
        public IActionResult Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
