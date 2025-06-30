using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data;
using SolingenOriginalsToptanci.Models.Entities;

var builder = WebApplication.CreateBuilder(args);

// DbContext ve Identity yapılandırması
builder.Services.AddDbContext<SolingenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = true;
})
    .AddEntityFrameworkStores<SolingenContext>()
    .AddDefaultTokenProviders();

// Cookie ayarları
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Area route — Area varsa çalışır
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Area olmayan controllerlar için default route
app.MapControllerRoute(
    name: "default_no_area",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Eğer istersen, admin area için ayrı default route koyabilirsin (opsiyonel)
// app.MapControllerRoute(
//     name: "default_with_area",
//     pattern: "{area=Admin}/{controller=Product}/{action=Index}/{id?}");

app.Run();
