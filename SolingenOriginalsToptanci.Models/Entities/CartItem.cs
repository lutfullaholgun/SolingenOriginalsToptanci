namespace SolingenOriginalsToptanci.Models.Entities
{
    public class CartItem
    {
        public int Id { get; set; }  // Primary Key

        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public string? Name { get; set; }
        public string? Model { get; set; }
        public string? Color { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string CustomerId { get; set; } = string.Empty;
    }
}
