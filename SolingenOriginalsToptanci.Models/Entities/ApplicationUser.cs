using Microsoft.AspNetCore.Identity;

namespace SolingenOriginalsToptanci.Models.Entities;
public class ApplicationUser : IdentityUser
{
    // Bu alanları ekleyin
    public string FullName { get; set; }  // Veritabanındaki NOT NULL sütun için
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
