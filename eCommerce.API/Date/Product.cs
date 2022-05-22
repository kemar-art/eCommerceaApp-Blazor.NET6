using System;
using System.Collections.Generic;

namespace eCommerce.API.Date
{
    public partial class Product
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Discription { get; set; }
        public string? Status { get; set; }
        public string? ImageUrl { get; set; }
        public string? Size { get; set; }
        public decimal? Price { get; set; }
        public int? Qty { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual Supplier? Supplier { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
