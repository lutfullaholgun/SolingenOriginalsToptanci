using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.Data.Interfaces;
using SolingenOriginalsToptanci.Models;
using SolingenOriginalsToptanci.WebUI.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SolingenOriginalsToptanci.Extensions;

namespace SolingenOriginalsToptanci.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Product> _productRepository;

        public HomeController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _productRepository.GetAllAsync();

            var favoriteIds = HttpContext.Session.Get<List<int>>("FavoriteProductIds") ?? new List<int>();

            var featuredProducts = allProducts
                // IsFeatured filtresi kaldýrýldý
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl,
                    IsFavorite = favoriteIds.Contains(p.Id)
                }).ToList();

            var viewModel = new HomeViewModel
            {
                FeaturedProducts = featuredProducts
            };

            return View(viewModel);
        }
    }
}
