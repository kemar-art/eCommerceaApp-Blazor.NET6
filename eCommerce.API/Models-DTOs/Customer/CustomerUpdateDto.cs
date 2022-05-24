using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Customer
{
    public class CustomerUpdateDto : BaseDto
    {
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
