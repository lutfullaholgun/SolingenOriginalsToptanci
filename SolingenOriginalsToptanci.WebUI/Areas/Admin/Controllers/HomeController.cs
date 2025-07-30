// Areas/Admin/Controllers/HomeController.cs
using Microsoft.AspNetCore.Mvc;

namespace SolingenOriginalsToptanci.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
