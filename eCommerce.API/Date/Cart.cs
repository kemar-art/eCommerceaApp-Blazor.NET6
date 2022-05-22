using System;
using System.Collections.Generic;

namespace eCommerce.API.Date
{
    public partial class Cart
    {
        public Cart()
        {
            CartItems = new HashSet<CartItem>();
        }

        public int Id { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
