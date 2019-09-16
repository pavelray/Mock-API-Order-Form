using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class BillingAddress : Address
    {
        public int Id { get; set; }
        public bool IsSameAsShipping { get; set; }
        public int FormId { get; set; }
    }
}
