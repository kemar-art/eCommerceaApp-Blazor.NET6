using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Cart
{
    public class CartReadOnlyDto : BaseDto
    {
        [Required]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
