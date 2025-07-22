using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Category
{
    public class DeleteCategoryVM
    {
        [Required(ErrorMessage = "The ID is required.")]
        public string Id { get; set; }
    }
}
