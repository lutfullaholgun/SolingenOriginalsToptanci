using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolingenOriginalsToptanci.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Model { get; set; }

        public string? Color { get; set; }

        [Required]
        public decimal Price { get; set; }

        public int Stock { get; set; }

        public string? Description { get; set; }

        public bool IsFeatured { get; set; }

        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile? ImageFile { get; set; }

        // Kategori ilişkisi
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
