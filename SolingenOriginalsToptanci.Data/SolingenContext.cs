using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Models.Entities;

namespace SolingenOriginalsToptanci.Data
{
    public class SolingenContext : DbContext
    {
        public SolingenContext(DbContextOptions<SolingenContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CartItem> CartItems { get; set; } // <-- Eksik olan satır
    }
}
