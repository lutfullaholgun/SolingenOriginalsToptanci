using System.ComponentModel.DataAnnotations;

namespace SolingenOriginalsToptanci.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }

        // İstersen açıklama vs. eklenebilir

        // Bu satır, ilişki için (tercihi sana kalmış)
        public ICollection<Product>? Products { get; set; }
    }
}
