using System.ComponentModel.DataAnnotations;

namespace RegisterOfProducts.Models
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter the user's current password")]
        public string CurrentPassword { get; set; }
        [Required(ErrorMessage = "Enter new user password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm new user password")]
        [Compare("NewPassword", ErrorMessage = "Password does not match the new password")]
        public string ConfirmNewPassword { get; set; }
    }
}
