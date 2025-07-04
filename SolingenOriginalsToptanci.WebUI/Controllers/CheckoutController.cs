using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.Models.Entities;
using SolingenOriginalsToptanci.Data; // DbContext varsa
using System.Security.Claims;

namespace SolingenOriginalsToptanci.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly SolingenContext _context;

        public CheckoutController(SolingenContext context)
        {
            _context = context;
        }

        // GET: /Checkout
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account"); // Kullanıcı girişi yoksa login sayfasına yönlendir
            }

            var cartItems = _context.CartItems
                .Where(c => c.CustomerId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                TempData["Message"] = "Sepetiniz boş.";
                return RedirectToAction("Index", "Cart");
            }

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult PlaceOrder()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var cartItems = _context.CartItems
                .Where(c => c.CustomerId == userId)
                .ToList();

            if (!cartItems.Any())
            {
                TempData["Message"] = "Sepetiniz boş.";
                return RedirectToAction("Index", "Cart");
            }

            // Burada sipariş kaydetme işlemi yapılacak (sipariş tablosu vs.)
            // Şimdilik sepeti temizleyelim:

            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            TempData["Success"] = "Siparişiniz başarıyla oluşturuldu. Teşekkürler!";
            return RedirectToAction("Index", "Home");
        }
    }
}
