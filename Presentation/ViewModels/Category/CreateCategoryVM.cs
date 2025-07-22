using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Category
{
    public class CreateCategoryVM
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(20, ErrorMessage = "Category name cannot exceed 100 characters.")]
        public string? Name { get; set; }
    }
}
