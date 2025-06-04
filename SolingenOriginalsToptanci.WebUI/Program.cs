using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.WebUI.Data;
using SolingenOriginalsToptanci.WebUI.Models;
using SolingenOriginalsToptanci.Data;
using SolingenOriginalsToptanci.Data.Interfaces;
using SolingenOriginalsToptanci.Data.Repositories;
using Rotativa.AspNetCore;
using SolingenOriginalsToptanci.Models;

var builder = WebApplication.CreateBuilder(args);

// 1) Kimlik (Identity) veritabaný baðlantýsý
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2) Identity sisteminin kurulumu (kullanýcý kimliði & rol yönetimi)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3) Kimlik doðrulama çerez ayarlarý
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

// 4) Ürün veritabaný (SolingenContext) baðlantýsý
builder.Services.AddDbContext<SolingenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 5) Generic Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// 6) Session yapýlandýrmasý
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// 7) MVC yapýlandýrmasý
builder.Services.AddControllersWithViews();

var app = builder.Build();

// 8) Hata yönetimi
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();

// 9) Kimlik & yetkilendirme middleware'leri
app.UseAuthentication();
app.UseAuthorization();

// 10) Session middleware'i
app.UseSession();

// 11) Rotativa (PDF çýktýsý) desteði
RotativaConfiguration.Setup(app.Environment.WebRootPath, "Rotativa");

// 12) Varsayýlan route tanýmý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task CreateRolesAndAdminUserAsync(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    string[] roles = { "Admin", "User" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    var adminEmail = "admin@solingen.com";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var user = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            FullName = "Admin User"
        };
        var result = await userManager.CreateAsync(user, "Admin123*");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, "Admin");
        }
    }
}

await CreateRolesAndAdminUserAsync(app);