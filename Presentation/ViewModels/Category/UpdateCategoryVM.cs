using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Category
{
    public class UpdateCategoryVM
    {
        [Required(ErrorMessage ="The name is requried.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "The id is required.")]
        public Guid Id { get; set; }
    }
}
