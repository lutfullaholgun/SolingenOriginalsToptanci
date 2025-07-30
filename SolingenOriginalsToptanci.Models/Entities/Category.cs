using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolingenOriginalsToptanci.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Slug zorunludur.")]
        public string Slug { get; set; } = string.Empty;

        public string? GroupName { get; set; }

        public int? ParentCategoryId { get; set; }

        public virtual Category? ParentCategory { get; set; }

        public virtual ICollection<Category> SubCategories { get; set; }

        public Category()
        {
            SubCategories = new HashSet<Category>();
        }
    }
}
