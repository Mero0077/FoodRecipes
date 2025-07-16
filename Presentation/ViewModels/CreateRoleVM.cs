using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels
{
    public class CreateRoleVM
    {
        [Required]
        public string Name { get; set; }
    }
}
