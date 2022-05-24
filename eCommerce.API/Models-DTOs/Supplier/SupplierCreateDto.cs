using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Supplier
{
    public class SupplierCreateDto
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
