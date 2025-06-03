using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.Data.Interfaces;
using SolingenOriginalsToptanci.Models;
using SolingenOriginalsToptanci.WebUI.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var featuredProducts = allProducts
                .Where(p => p.IsFeatured)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ImageUrl
                }).ToList();

            var viewModel = new HomeViewModel
            {
                FeaturedProducts = featuredProducts
            };

            return View(viewModel);
        }
    }
}
