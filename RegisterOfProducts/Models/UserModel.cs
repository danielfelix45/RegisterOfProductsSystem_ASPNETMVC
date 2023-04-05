using RegisterOfProducts.Enums;
using System.ComponentModel.DataAnnotations;

namespace RegisterOfProducts.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User name is required")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "User email is required")]
        [EmailAddress(ErrorMessage = "Enter valid email")]
        [MaxLength(80)]
        public string Email { get; set; }
        [Required(ErrorMessage = "User password is required")]
        [MaxLength(80)]
        public string Password { get; set; }
        [Required(ErrorMessage = "User login is required")]
        [MaxLength(50)]
        public string Login { get; set; }
        [Required(ErrorMessage = "User profile is required")]
        public ProfileEnum Profile { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime? DateUpdate { get; set; }

        public bool ValidPassword(string password)
        {
            return Password == password;
        }
    }
}
