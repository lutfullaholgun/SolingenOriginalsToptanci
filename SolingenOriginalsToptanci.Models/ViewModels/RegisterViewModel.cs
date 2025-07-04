using System.ComponentModel.DataAnnotations;

public class RegisterModel
{
    [Required(ErrorMessage = "E-posta zorunludur")]
    [EmailAddress(ErrorMessage = "Geçersiz e-posta formatı")]
    [Display(Name = "E-Posta")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Şifre zorunludur")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalı")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Şifre Tekrar")]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
    public string ConfirmPassword { get; set; }

    [Required(ErrorMessage = "Ad zorunludur")]
    [Display(Name = "Ad")]
    [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$", ErrorMessage = "Sadece harf kullanabilirsiniz")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Soyad zorunludur")]
    [Display(Name = "Soyad")]
    [RegularExpression(@"^[a-zA-ZğüşıöçĞÜŞİÖÇ\s]+$", ErrorMessage = "Sadece harf kullanabilirsiniz")]
    public string LastName { get; set; }

    [MustBeTrue(ErrorMessage = "Koşulları kabul etmelisiniz")]
    [Display(Name = "Kullanım Koşulları")]
    public bool AgreeToTerms { get; set; }
}

// MustBeTrueAttribute sınıfını ayrı bir yerde tanımlayın (aynı namespace içinde)
public class MustBeTrueAttribute : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        return value is bool && (bool)value;
    }
}