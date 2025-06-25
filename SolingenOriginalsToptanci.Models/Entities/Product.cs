// Models/Entities/Product.cs (Mevcut modeliniz)
// Bu dosya değişmeden kalacak, çünkü siz zaten bunu sağlamıştınız.
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http; // IFormFile için

namespace SolingenOriginalsToptanci.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string? Model { get; set; }

        [StringLength(50)]
        public string? Color { get; set; }

        [Range(0, double.MaxValue)] // Fiyat 0'dan büyük eşit olabilir. Eğer 0.01'den başlatmak isterseniz Range(0.01, double.MaxValue) yapın.
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsFeatured { get; set; }

        [StringLength(1000)]
        public string? Description { get; set; }

        [Range(0, int.MaxValue)] // Stok negatif olamaz.
        public int Stock { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}