using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolingenOriginalsToptanci.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public int? ParentCategoryId { get; set; }  // Nullable, çünkü ana kategori olabilir
        [ForeignKey("ParentCategoryId")]
        public Category ParentCategory { get; set; }

        public ICollection<Category> SubCategories { get; set; }  // Alt kategoriler

        public Category()
        {
            SubCategories = new HashSet<Category>();
        }
    }
}


