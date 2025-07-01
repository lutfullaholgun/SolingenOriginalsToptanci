using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

public class CategoryViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı zorunludur.")]
    public string Name { get; set; }

    public int? ParentCategoryId { get; set; }

    public List<SelectListItem>? Categories { get; set; }
}
