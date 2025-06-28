using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data;
using SolingenOriginalsToptanci.Models.Entities;

[Authorize(Roles = "Admin")]
[Area("Admin")]
public class ProductController : Controller
{
    private readonly SolingenContext _context;
    private readonly IWebHostEnvironment _env;

    public ProductController(SolingenContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    public IActionResult Index() => View(_context.Products.Include(p => p.Category).ToList());

    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            if (product.ImageFile != null)
            {
                var fileName = Guid.NewGuid() + Path.GetExtension(product.ImageFile.FileName);
                var filePath = Path.Combine(_env.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await product.ImageFile.CopyToAsync(stream);
                }

                product.ImageUrl = "/uploads/" + fileName;
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        return View(product);
    }
}
