using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.Data.Interfaces;
using SolingenOriginalsToptanci.Models;
using SolingenOriginalsToptanci.WebUI.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class ProductsController : Controller
{
    private readonly IRepository<Product> _productRepo;
    private const string CartKey = "Cart";

    public ProductsController(IRepository<Product> productRepo)
    {
        _productRepo = productRepo;
    }

    // Ürün listeleme ve filtreleme
    public async Task<IActionResult> Index(string? color, string? model)
    {
        var all = await _productRepo.GetAllAsync();

        var filtered = all
            .Where(p => string.IsNullOrEmpty(color) || p.Color.Equals(color, StringComparison.OrdinalIgnoreCase))
            .Where(p => string.IsNullOrEmpty(model) || p.Model.Equals(model, StringComparison.OrdinalIgnoreCase))
            .ToList();

        ViewBag.Colors = all.Select(p => p.Color).Distinct();
        ViewBag.Models = all.Select(p => p.Model).Distinct();
        ViewBag.SelectedColor = color;
        ViewBag.SelectedModel = model;

        return View(filtered);
    }

    // Ürün detay sayfası
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        return View(product);
    }

    // Ürünü sepete ekle
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int productId)
    {
        var product = await _productRepo.GetByIdAsync(productId);
        if (product == null)
            return NotFound();

        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartKey) ?? new List<CartItem>();

        var item = cart.FirstOrDefault(c => c.ProductId == productId);
        if (item != null)
            item.Quantity++;
        else
        {
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Color = product.Color,
                Model = product.Model,
                Price = product.Price,
                Quantity = 1
            });
        }

        HttpContext.Session.SetObjectAsJson(CartKey, cart);
        return RedirectToAction(nameof(Index));
    }

    // GET: Yeni ürün oluşturma sayfası
    public IActionResult Create()
    {
        return View();
    }

    // POST: Yeni ürün oluşturur
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
            return View(product);

        // Resim yükleme
        if (product.ImageFile != null && product.ImageFile.Length > 0)
        {
            var wwwRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
            if (!Directory.Exists(wwwRootPath))
                Directory.CreateDirectory(wwwRootPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(product.ImageFile.FileName);
            var filePath = Path.Combine(wwwRootPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await product.ImageFile.CopyToAsync(stream);
            }

            product.ImageUrl = "/images/" + fileName;
        }

        await _productRepo.AddAsync(product);
        return RedirectToAction(nameof(Index));
    }

    // GET: Ürün düzenleme sayfası
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        return View(product);
    }

    // POST: Ürün düzenlemesini kaydeder
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id)
            return BadRequest();

        if (!ModelState.IsValid)
            return View(product);

        _productRepo.Update(product);
        await _productRepo.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Ürün silme onayı
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        return View(product);
    }

    // POST: Ürünü siler
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        if (product == null)
            return NotFound();

        _productRepo.Delete(product);
        await _productRepo.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}
