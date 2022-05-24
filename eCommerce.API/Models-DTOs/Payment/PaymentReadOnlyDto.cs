using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Payment
{
    public class PaymentReadOnlyDto : BaseDto
    {
        [Required]
        public string PaymentType { get; set; }
        [Required]
        public int OrderId { get; set; }
    }
}
