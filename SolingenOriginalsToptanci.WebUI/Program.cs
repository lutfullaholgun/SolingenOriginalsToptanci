using System;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data;
using SolingenOriginalsToptanci.Data.Interfaces;
using SolingenOriginalsToptanci.Data.Repositories;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);  // 1) Builder burada tanýmlanýr

// 2) Service kayýtlarý
// -- EF Core: Baðlantý dizesini doðru þekilde alýyoruz
builder.Services.AddDbContext<SolingenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -- Repository: Generic repository kaydý
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// -- Session: Session'ý etkinleþtiriyoruz
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // 30 dakikalýk oturum süresi
    options.Cookie.HttpOnly = true;  // Sadece HTTP istekleriyle eriþilebilir olmalý
});

// -- MVC: MVC yapýlandýrmasý
builder.Services.AddControllersWithViews();

// 3) Uygulamayý oluþtur
var app = builder.Build();

// 4) Middleware ve routing
if (!app.Environment.IsDevelopment())
{
    // Üretim ortamýnda hata iþleme
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();  // Statik dosyalarý sunma

app.UseRouting();  // Yönlendirmeyi etkinleþtir

app.UseSession();  // Session'ý etkinleþtiriyoruz

// MVC yönlendirme yapýlandýrmasý
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulamayý çalýþtýr
app.Run();
