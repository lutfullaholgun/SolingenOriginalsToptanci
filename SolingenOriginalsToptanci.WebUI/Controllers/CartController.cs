using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.Models;
using SolingenOriginalsToptanci.Data.Interfaces;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SolingenOriginalsToptanci.Controllers
{
    public class CartController : Controller
    {
        private readonly IRepository<Product> _productRepository;

        public CartController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var cartItems = GetCartFromSession();
            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                return NotFound();

            var cart = GetCartFromSession();
            var existingItem = cart.Find(x => x.ProductId == product.Id);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    Product = product,
                    Quantity = 1
                });
            }

            SaveCartToSession(cart);
            return RedirectToAction("Index", "Products");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCartFromSession();
            cart.RemoveAll(x => x.ProductId == productId);
            SaveCartToSession(cart);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetCartCount()
        {
            var cart = GetCartFromSession();
            var count = 0;
            foreach (var item in cart)
                count += item.Quantity;

            return Json(count);
        }

        // === Session helpers ===
        private List<CartItem> GetCartFromSession()
        {
            var json = HttpContext.Session.GetString("Cart");
            if (string.IsNullOrEmpty(json))
                return new List<CartItem>();

            return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }

        private void SaveCartToSession(List<CartItem> cart)
        {
            var json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString("Cart", json);
        }
    }
}
