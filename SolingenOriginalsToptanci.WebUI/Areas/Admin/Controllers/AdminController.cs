using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.Models.Entities;
using SolingenOriginalsToptanci.Models.ViewModels;

namespace SolingenOriginalsToptanci.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class AdminController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            ILogger<AdminController> logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
        }

        [HttpGet("Login")]
        [AllowAnonymous]
        public IActionResult AdminLogin(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User))
            {
                return RedirectToAction("Index", "Dashboard");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // Sadece admin email pattern'i ile giriş
                if (!model.Email.EndsWith("@solingen.com"))
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz admin giriş denemesi");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        _logger.LogInformation("Admin giriş yaptı: {Email}", model.Email);
                        return LocalRedirect(returnUrl ?? "/Admin/Dashboard");
                    }
                    await _signInManager.SignOutAsync();
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Admin hesabı kilitlendi: {Email}", model.Email);
                    return RedirectToAction("Lockout");
                }
                ModelState.AddModelError(string.Empty, "Geçersiz giriş denemesi");
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Dashboard")]
        public IActionResult Dashboard()
        {
            return View();
        }

        [HttpPost("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Admin çıkış yaptı");
            return RedirectToAction("AdminLogin");
        }
    }
}