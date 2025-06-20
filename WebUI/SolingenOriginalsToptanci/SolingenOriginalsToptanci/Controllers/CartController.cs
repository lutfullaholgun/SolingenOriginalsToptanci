using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data;
using SolingenOriginalsToptanci.Models.Entities;

public class CartController : Controller
{
    private readonly SolingenContext _context;

    public CartController(SolingenContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var customerId = "guest"; // Giriş yapılmamışsa örnek id
        var cartItems = await _context.CartItems
            .Include(c => c.Product)
            .Where(c => c.CustomerId == customerId)
            .ToListAsync();

        return View(cartItems);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId)
    {
        var customerId = "guest";

        var cartItem = await _context.CartItems
            .FirstOrDefaultAsync(c => c.ProductId == productId && c.CustomerId == customerId);

        if (cartItem == null)
        {
            cartItem = new CartItem
            {
                ProductId = productId,
                CustomerId = customerId,
                Quantity = 1
            };
            _context.CartItems.Add(cartItem);
        }
        else
        {
            cartItem.Quantity++;
        }

        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Product");
    }

    public async Task<IActionResult> Remove(int id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem != null)
        {
            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Index");
    }
}
