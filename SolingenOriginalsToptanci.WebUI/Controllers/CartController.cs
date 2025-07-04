using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

namespace SolingenOriginalsToptanci.WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly SolingenContext _context;

        public CartController(SolingenContext context)
        {
            _context = context;
        }

        // Sepet sayfası
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Challenge(); // Giriş yoksa yönlendir

            var cartItems = await _context.CartItems
                .Include(c => c.Product) // Ürün bilgilerini de al
                .Where(c => c.CustomerId == userId)
                .OrderBy(c => c.Id)
                .ToListAsync();

            return View(cartItems);
        }

        // Sepette ürün adedi güncelleme (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int cartItemId, int quantity)
        {
            if (quantity < 1)
                quantity = 1;

            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (cartItem.CustomerId != userId)
                return Forbid();

            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // Sepetten ürün kaldırma (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (cartItem.CustomerId != userId)
                return Forbid();

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
