using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class FormData
    {
        public int Id { get; set; }
        public int Step { get; set; }
        public DateTime Created_Date { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
        public BillingAddress BillingAddress { get; set; }
        public OrderDetails OrderDetails { get; set; }
        public Specifications Specifications { get; set; }
    }
}
