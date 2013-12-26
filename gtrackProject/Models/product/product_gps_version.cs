using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class ProductGpsVersion
    {
        public ProductGpsVersion()
        {
            this.Orders = new List<Order>();
            this.ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
