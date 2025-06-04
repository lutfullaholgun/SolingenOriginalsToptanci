
using Microsoft.AspNetCore.Identity;

namespace SolingenOriginalsToptanci.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } // Ad Soyad bilgisi burada saklanır
    }
}
