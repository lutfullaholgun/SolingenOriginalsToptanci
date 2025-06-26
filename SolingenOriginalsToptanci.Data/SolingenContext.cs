// SolingenOriginalsToptanci.Data/SolingenContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; // Bu using'i ekleyin!
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Models.Entities; // Bu satırı ekleyin!

namespace SolingenOriginalsToptanci.Data
{
    // *** ÖNEMLİ DEĞİŞİKLİK BURADA: DbContext yerine IdentityDbContext<ApplicationUser> kullanıyoruz ***
    public class SolingenContext : IdentityDbContext<ApplicationUser>
    {
        public SolingenContext(DbContextOptions<SolingenContext> options)
            : base(options)
        {
        }

        // Mevcut DbSet'leriniz
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }


        // Eklediğiniz CartItem DbSet'i
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Identity için gerekli! Bu satırı UNUTMAYIN!

            // Model oluşturma ile ilgili kendi özel yapılandırmalarınız varsa buraya ekleyin.
            // Örnek: modelBuilder.Entity<Product>().Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
}