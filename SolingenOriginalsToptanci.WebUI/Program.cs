using System;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Data;
using SolingenOriginalsToptanci.Data.Interfaces;
using SolingenOriginalsToptanci.Data.Repositories;
using Rotativa.AspNetCore;

var builder = WebApplication.CreateBuilder(args);  // 1) Builder burada tan�mlan�r

// 2) Service kay�tlar�
// -- EF Core: Ba�lant� dizesini do�ru �ekilde al�yoruz
builder.Services.AddDbContext<SolingenContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// -- Repository: Generic repository kayd�
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// -- Session: Session'� etkinle�tiriyoruz
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);  // 30 dakikal�k oturum s�resi
    options.Cookie.HttpOnly = true;  // Sadece HTTP istekleriyle eri�ilebilir olmal�
});

// -- MVC: MVC yap�land�rmas�
builder.Services.AddControllersWithViews();

// 3) Uygulamay� olu�tur
var app = builder.Build();

// 4) Middleware ve routing
if (!app.Environment.IsDevelopment())
{
    // �retim ortam�nda hata i�leme
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();  // Statik dosyalar� sunma

app.UseRouting();  // Y�nlendirmeyi etkinle�tir

app.UseSession();  // Session'� etkinle�tiriyoruz

// MVC y�nlendirme yap�land�rmas�
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulamay� �al��t�r
app.Run();
