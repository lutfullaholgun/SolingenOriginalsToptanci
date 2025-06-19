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

        // Buraya veritabanı tablolarına karşılık gelen DbSet'leri ekle
        // Örnek:
        // public DbSet<Product> Products { get; set; }
        // public DbSet<Customer> Customers { get; set; }
    }
}
