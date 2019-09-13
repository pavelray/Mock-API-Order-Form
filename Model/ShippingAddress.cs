using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ShippingAddress : Address
    {
        public int Id { get; set; }
        public int OrderNo { get; set; }

    }
}
