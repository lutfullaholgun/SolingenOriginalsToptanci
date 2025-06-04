using System.ComponentModel.DataAnnotations;

namespace SolingenOriginalsToptanci.WebUI.Models.ViewModels
{
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Email zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir email giriniz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
