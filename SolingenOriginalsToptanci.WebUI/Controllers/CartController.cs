using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.WebUI.Helpers;
using SolingenOriginalsToptanci.WebUI.Models;
using Rotativa.AspNetCore;

public class CartController : Controller
{
    private const string CartKey = "Cart";

    // GET: /Cart
    public IActionResult Index()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartKey)
                   ?? new List<CartItem>();

        ViewBag.Total = cart.Sum(i => i.Price * i.Quantity);
        return View(cart);
    }

    // GET: /Cart/DownloadInvoice
    public IActionResult DownloadInvoice()
    {
        var cart = HttpContext.Session.GetObjectFromJson<List<CartItem>>(CartKey)
                   ?? new List<CartItem>();

        ViewBag.Total = cart.Sum(i => i.Price * i.Quantity);

        // Invoice.cshtml görünümü kullanılarak PDF oluşturulur
        return new ViewAsPdf("Invoice", cart)
        {
            FileName = "invoice.pdf",
            PageSize = Rotativa.AspNetCore.Options.Size.A4
        };
    }
}
