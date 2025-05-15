using System.IO;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data;
using SolingenOriginalsToptanci.Data.Interfaces;
using SolingenOriginalsToptanci.Data.Repositories;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);  // 1) builder burada tanýmlanýr

// 2) Service kayýtlarý
// -- EF Core
builder.Services.AddDbContext<SolingenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// -- Repository
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));



// -- Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
});

// -- MVC
builder.Services.AddControllersWithViews();

// 3) Uygulamayý oluþtur
var app = builder.Build();

// 4) Middleware ve routing
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();    // Session’ý burada etkinleþtiriyoruz

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
