using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SolingenOriginalsToptanci.Models.Entities;
using SolingenOriginalsToptanci.Models.ViewModels;

namespace SolingenOriginalsToptanci.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/[controller]")]
    public class AdminAccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AdminAccountController> _logger;

        public AdminAccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<AdminAccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [HttpGet("Login")]
        [AllowAnonymous]
        public IActionResult AdminLogin()
        {
            if (_signInManager.IsSignedIn(User))
                return RedirectToAction("Dashboard");

            return View();
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!model.Email.EndsWith("@solingen.com"))
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz admin girişi");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (await _userManager.IsInRoleAsync(user, "Admin"))
                    {
                        _logger.LogInformation("Admin girişi: {Email}", model.Email);
                        return RedirectToAction("Dashboard");
                    }
                    await _signInManager.SignOutAsync();
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Kilitli admin hesabı: {Email}", model.Email);
                    return RedirectToAction("Lockout");
                }
                ModelState.AddModelError(string.Empty, "Geçersiz giriş");
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