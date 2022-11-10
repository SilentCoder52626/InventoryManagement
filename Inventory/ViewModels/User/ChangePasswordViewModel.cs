using System.ComponentModel.DataAnnotations;

namespace Inventory.ViewModels.User
{
    public class ChangePasswordViewModel
    {
        public string? UserId { get; set; }
        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Compare("NewPassword", ErrorMessage = "Password donot match")]
        public string ConfirmPassword { get; set; }
    }
}
