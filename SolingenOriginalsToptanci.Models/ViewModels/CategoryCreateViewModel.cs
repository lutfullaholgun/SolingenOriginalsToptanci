using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SolingenOriginalsToptanci.Models.ViewModels
{
    public class CategoryCreateViewModel
    {
        [Required(ErrorMessage = "Kategori adı zorunludur.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Slug zorunludur.")]
        public string Slug { get; set; }

        public string? GroupName { get; set; }

        public int? ParentCategoryId { get; set; }

        public List<SelectListItem> PossibleParents { get; set; } = new List<SelectListItem>();
    }
}
