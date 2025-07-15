using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Presentation.ViewModels.User
{
    public class ResetPasswordVM
    {
        [Required]
        public string UserName  { get; set; }
        [Required]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Confirm Password required")]
        [CompareAttribute("NewPassword", ErrorMessage = "Password doesn't match.")]
        public string NewPassword { get; set; }
    }
}
