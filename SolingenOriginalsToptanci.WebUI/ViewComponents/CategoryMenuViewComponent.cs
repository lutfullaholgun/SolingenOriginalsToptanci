using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data;

namespace SolingenOriginalsToptanci.ViewComponents
{
    public class CategoryMenuViewComponent : ViewComponent
    {
        private readonly SolingenContext _context;

        public CategoryMenuViewComponent(SolingenContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.SubCategories)
                .Where(c => c.ParentCategoryId == null)
                .ToListAsync();

            return View(categories);
        }
    }
}
