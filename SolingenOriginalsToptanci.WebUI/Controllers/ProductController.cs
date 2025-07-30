using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data;
using SolingenOriginalsToptanci.Models.Entities;

namespace SolingenOriginalsToptanci.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly SolingenContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(SolingenContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products.ToListAsync();
            return View(products);
        }


        // HomeController veya ProductController içinde
        public IActionResult Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                ViewBag.Message = "Lütfen arama terimi giriniz.";
                return View(new List<Product>());
            }

            var products = _context.Products
                            .Where(p => p.Name.Contains(query) || p.Description.Contains(query))
                            .ToList();

            if (!products.Any())
            {
                ViewBag.Message = "Aramanıza uygun ürün bulunamadı.";
            }

            return View(products);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (ModelState.IsValid)
            {
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid() + Path.GetExtension(product.ImageFile.FileName);
                    var imagesPath = Path.Combine(_env.WebRootPath, "images");

                    if (!Directory.Exists(imagesPath))
                    {
                        Directory.CreateDirectory(imagesPath);
                    }

                    var filePath = Path.Combine(imagesPath, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await product.ImageFile.CopyToAsync(stream);
                    }

                    product.ImageUrl = "/images/" + fileName;
                }

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(product);
        }
    }
}
