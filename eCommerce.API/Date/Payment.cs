using System;
using System.Collections.Generic;

namespace eCommerce.API.Date
{
    public partial class Payment
    {
        public int Id { get; set; }
        public string? PaymentType { get; set; }
        public int? OrderId { get; set; }

        public virtual Order? Order { get; set; }
    }
}
