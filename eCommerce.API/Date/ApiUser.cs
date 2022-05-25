using Microsoft.AspNetCore.Identity;

namespace eCommerce.API.Date
{
    public class ApiUser : IdentityUser
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool isActive { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
