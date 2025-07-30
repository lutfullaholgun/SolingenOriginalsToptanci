using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using SolingenOriginalsToptanci.Models.Entities;
using System;
using System.Threading.Tasks;

namespace SolingenOriginalsToptanci.Data
{
    public static class SeedData
    {
        public static async Task EnsureAdminAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string adminEmail = "yonetici@solingen.com";
            string adminPassword = "Test123!";

            // 1. Admin rolü yoksa oluştur
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // 2. Admin kullanıcı yoksa oluştur
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Site",
                    LastName = "Yöneticisi",
                    FullName = "Site Yöneticisi"  // BURAYI EKLE
                };

                var result = await userManager.CreateAsync(newAdmin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
        }
    }
}
