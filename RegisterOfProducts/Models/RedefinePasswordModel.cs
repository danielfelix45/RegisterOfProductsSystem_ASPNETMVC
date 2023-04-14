using System.ComponentModel.DataAnnotations;

namespace RegisterOfProducts.Models
{
    public class RedefinePasswordModel
    {
        [Required(ErrorMessage = "Enter Login")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Enter E-mail")]
        public string Email { get; set; }
    }
}
