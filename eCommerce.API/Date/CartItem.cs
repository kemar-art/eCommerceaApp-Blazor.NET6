using System;
using System.Collections.Generic;

namespace eCommerce.API.Date
{
    public partial class CartItem
    {
        public int Id { get; set; }
        public int? Qty { get; set; }
        public decimal? Discount { get; set; }
        public int? CartId { get; set; }
        public int? ProductId { get; set; }

        public virtual Cart? Cart { get; set; }
        public virtual Product? Product { get; set; }
    }
}
