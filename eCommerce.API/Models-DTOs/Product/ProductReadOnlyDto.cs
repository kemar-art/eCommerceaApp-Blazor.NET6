using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.Product
{
    public class ProductReadOnlyDto : BaseDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Discription { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Qty { get; set; }
        [Required]
        public int SupplierId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}
