using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Order
{
    public class OrderUpdateDto : BaseDto
    {
        public DateTime? DateOfOrder { get; set; }
        [Required]
        public decimal Price { get; set; }
        public decimal? Discount { get; set; }
        [Required]
        public int CustomerId { get; set; }
    }
}
