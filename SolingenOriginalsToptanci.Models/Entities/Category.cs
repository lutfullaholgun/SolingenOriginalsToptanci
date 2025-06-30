using System.Collections.Generic;

namespace SolingenOriginalsToptanci.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } // Kategori adı örn: "Meyve Bıçakları"
        public string Slug { get; set; } // URL uyumlu "meyve-bicaklari"

        public ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
    }
}
