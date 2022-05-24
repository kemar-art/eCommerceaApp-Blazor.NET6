using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Categorey
{
    public class CategoreyReadOnlyDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
