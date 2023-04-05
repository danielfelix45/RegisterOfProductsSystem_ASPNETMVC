using System.ComponentModel.DataAnnotations;

namespace RegisterOfProducts.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter Login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }
    }
}
