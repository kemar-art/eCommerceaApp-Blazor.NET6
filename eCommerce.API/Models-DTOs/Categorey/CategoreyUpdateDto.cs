using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Categorey
{
    public class CategoreyUpdateDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
    }
}
