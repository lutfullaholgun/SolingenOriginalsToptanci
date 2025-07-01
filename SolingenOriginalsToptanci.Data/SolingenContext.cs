using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Models.Entities;

namespace SolingenOriginalsToptanci.Data
{
    public class SolingenContext : IdentityDbContext<ApplicationUser>
    {
        public SolingenContext(DbContextOptions<SolingenContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // MUTLAKA BU OLMALI!

            // İstersen buraya kendi entity konfigürasyonlarını ekleyebilirsin
        }
    }
}
