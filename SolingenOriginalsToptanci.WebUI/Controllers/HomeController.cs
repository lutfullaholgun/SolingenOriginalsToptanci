using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.Data.Interfaces;
using SolingenOriginalsToptanci.Models;
using SolingenOriginalsToptanci.WebUI.Models.ViewModels;
using System.Linq;
using System.Threading.Tasks;

public class HomeController : Controller
{
    private readonly IRepository<Product> _productRepository;

    public HomeController(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IActionResult> Index()
    {
        var products = await _productRepository.GetAllAsync();

        var viewModel = new HomeViewModel
        {
            FeaturedProducts = products
                .Take(3)
                .Select(p => new ProductViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Price = p.Price
                })
                .ToList()
        };

        return View(viewModel);
    }
}
