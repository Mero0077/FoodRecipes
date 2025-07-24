using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Roles
{
    public class CreateRoleVM
    {
        [Required]
        public string Name { get; set; }
    }
}
