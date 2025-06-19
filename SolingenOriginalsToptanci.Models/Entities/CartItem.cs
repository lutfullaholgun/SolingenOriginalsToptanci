namespace SolingenOriginalsToptanci.Models.Entities
{
    public class CartItem
    {
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
