using System.ComponentModel.DataAnnotations;

namespace SolingenOriginalsToptanci.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "E-posta zorunludur")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Ad zorunludur")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur")]
        public string LastName { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Koşulları kabul etmelisiniz")]
        public bool AgreeToTerms { get; set; }
    }
}