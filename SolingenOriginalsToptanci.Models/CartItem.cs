namespace SolingenOriginalsToptanci.Models
{

    public class CartItem
    {
        public Product? Product { get; set; }  // İlgili ürün bilgileri
        public int ProductId { get; set; }
        public string? Name { get; set; }        // Ürün adı
        public string? Model { get; set; }       // Ürün modeli
        public string? Color { get; set; }       // Ürün rengi
        public decimal Price { get; set; }      // Ürün fiyatı
        public int Quantity { get; set; }
    }
}