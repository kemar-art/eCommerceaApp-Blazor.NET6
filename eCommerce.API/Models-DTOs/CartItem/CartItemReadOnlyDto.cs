using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.CartItem
{
    public class CartItemReadOnlyDto : BaseDto
    {
        [Required]
        public int Qty { get; set; }
        public decimal Discount { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
    }
}
