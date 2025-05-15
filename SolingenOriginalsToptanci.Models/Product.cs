namespace SolingenOriginalsToptanci.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
    }

    public class CustomerInfo
    {
        public int Id { get; set; }
        public string FullNameOrCompany { get; set; }
    }
}
