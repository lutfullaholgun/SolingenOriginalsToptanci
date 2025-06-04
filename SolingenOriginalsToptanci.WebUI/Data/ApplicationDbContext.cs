using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SolingenOriginalsToptanci.Models;
using SolingenOriginalsToptanci.WebUI.Models;

namespace SolingenOriginalsToptanci.WebUI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Diğer DbSet'ler buraya eklenebilir
    }
}
