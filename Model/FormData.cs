using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class FormData
    {
        public ShippingAddress ShippingAddress { get; set; }
        public BillingAddress BillingAddress { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public Specifications Specifications { get; set; }
    }
}
