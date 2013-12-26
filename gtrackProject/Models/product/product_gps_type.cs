using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class ProductGpsType
    {
        public ProductGpsType()
        {
            this.ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        public string StatusNameTh { get; set; }
        public string StatusNameEn { get; set; }
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
