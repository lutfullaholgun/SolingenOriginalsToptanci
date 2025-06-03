using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SolingenOriginalsToptanci.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeatured { get; set; }

        public string Description { get; set; }

        public int Stock { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
