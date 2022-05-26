using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.User
{
    public class AuthUserDto : LoginUserDto
    {
        [Required]
        public string firstName { get; set; }

        [Required]
        public string lastName { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
