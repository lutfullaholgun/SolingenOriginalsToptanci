using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.Data.Interfaces;
using SolingenOriginalsToptanci.Models;
using SolingenOriginalsToptanci.WebUI.Helpers;
using SolingenOriginalsToptanci.WebUI.Models;

public class ProductsController : Controller
{
    private readonly IRepository<Product> _productRepo;
    private const string CartKey = "Cart";

    public ProductsController(IRepository<Product> productRepo)
        => _productRepo = productRepo;

    // GET: /Products?color=Red&model=Classic
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

    // POST: /Products/AddToCart
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddToCart(int productId)
    {
        var product = await _productRepo.GetByIdAsync(productId);
        if (product == null)
            return NotFound();

        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartKey)
                   ?? new List<CartItem>();

        var item = cart.FirstOrDefault(c => c.ProductId == productId);
        if (item != null)
            item.Quantity++;
        else
            cart.Add(new CartItem
            {
                ProductId = product.Id,
                Name = product.Name,
                Color = product.Color,
                Model = product.Model,
                Price = product.Price,
                Quantity = 1
            });

        HttpContext.Session.SetObjectAsJson(CartKey, cart);
        return RedirectToAction(nameof(Index));
    }
}
