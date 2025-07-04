using Microsoft.AspNetCore.Identity;

namespace SolingenOriginalsToptanci.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // Ek alanlar buraya
        public string FullName { get; set; } // Bu alanı senin eklemen gerekiyor
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
