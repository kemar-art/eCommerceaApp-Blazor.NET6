using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Categorey
{
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
