using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class OrderDetails
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public int FormId { get; set; }
    }
}
