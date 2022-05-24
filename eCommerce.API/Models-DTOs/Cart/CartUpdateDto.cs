using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Cart
{
    public class CartUpdateDto : BaseDto
    {
        [Required]
        public int CustomerId { get; set; }
    }
}
