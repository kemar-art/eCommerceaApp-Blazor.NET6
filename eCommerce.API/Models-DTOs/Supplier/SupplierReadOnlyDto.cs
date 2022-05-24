using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Supplier
{
    public class SupplierReadOnlyDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
