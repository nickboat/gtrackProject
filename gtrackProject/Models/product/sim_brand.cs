using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class SimBrand
    {
        public SimBrand()
        {
            this.ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        public string BrandName { get; set; }
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
