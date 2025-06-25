// SolingenOriginalsToptanci.Models.Entities/ApplicationUser.cs
using Microsoft.AspNetCore.Identity;

namespace SolingenOriginalsToptanci.Models.Entities
{
    // IdentityUser'dan türeyerek varsayılan kullanıcı alanlarına ekleme yapabilirsiniz.
    // Şimdilik ek alan eklemiyoruz, sadece türetiyoruz.
    public class ApplicationUser : IdentityUser
    {
        // Ek özellikler buraya eklenebilir, örneğin:
        // public string FullName { get; set; }
        // public DateTime DateOfBirth { get; set; }
    }
}