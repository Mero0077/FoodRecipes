using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Category
{
    public class UpdateCategoryVM
    {
        [Required(ErrorMessage ="The name is requried.")]
        public string Name { get; set; }
    }
}
