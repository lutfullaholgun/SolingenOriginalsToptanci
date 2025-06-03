namespace SolingenOriginalsToptanci.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; } // Ürün görseli için
        public string? Description { get; set; } // Açıklama
        public int Stock { get; set; } // Stok adedi
        public bool IsFeatured { get; set; }

    }

    public class CustomerInfo
    {
        public int Id { get; set; }
        public string FullNameOrCompany { get; set; }
    }
}
