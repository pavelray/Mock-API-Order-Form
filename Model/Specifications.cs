using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Specifications
    {
        public int Id { get; set; }
        public float ActualThickness { get; set; }
        public float ActualLength { get; set; }
        public float ActualWidth { get; set; }
        public string SeriesName { get; set; }
        public string Type { get; set; }
        public string Warranty { get; set; }
        public int OrderNo { get; set; }

    }
}
