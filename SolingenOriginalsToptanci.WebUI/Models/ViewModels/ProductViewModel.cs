namespace SolingenOriginalsToptanci.WebUI.Models.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool IsFavorite { get; set; }
    }
}