using System.ComponentModel.DataAnnotations;

namespace RegisterOfProducts.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Product name is required")]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Product description is required")]
        [MaxLength(150)]
        public string Description { get; set; }
        [Required(ErrorMessage = "Inform the price of the product")]
        public decimal Price { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
}
