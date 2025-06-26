using System.ComponentModel.DataAnnotations.Schema;

namespace SolingenOriginalsToptanci.Models.Entities
{
    public class FavoriteProduct
    {
        public int Id { get; set; } // Opsiyonel, istersen composite key yapabilirsin

        public string UserId { get; set; }  // IdentityUser Id'si

        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        // User ile ilişki opsiyonel:
        // [ForeignKey(nameof(UserId))]
        // public ApplicationUser User { get; set; }
    }
}
