namespace SolingenOriginalsToptanci.Models.Entities
{
    public class SubCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } // Örn: "1. Grup"
        public string Slug { get; set; } // URL uyumlu "1-grup"

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
