using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Models;

namespace SolingenOriginalsToptanci.Data
{
    // DbContext’inizin adı: SolingenContext
    public class SolingenContext : DbContext
    {
        // Constructor: Options alır, base’e iletir
        public SolingenContext(DbContextOptions<SolingenContext> options)
            : base(options)
        {
        }

        // Veritabanındaki tablolar:
        public DbSet<Product> Products { get; set; }
        public DbSet<CustomerInfo> Customers { get; set; }
    }
}
