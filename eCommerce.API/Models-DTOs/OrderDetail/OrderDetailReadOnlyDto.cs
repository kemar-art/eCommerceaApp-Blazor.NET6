using System.ComponentModel.DataAnnotations;

namespace eCommerce.API.Models_DTOs.OrderDetail
{
    public class OrderDetailReadOnlyDto : BaseDto
    {
        [Required]
        public int Qty { get; set; }
        [Required]
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        [Required]
        public decimal? Total { get; set; }
        public DateTime? Date { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ProductId { get; set; }
    }
}
