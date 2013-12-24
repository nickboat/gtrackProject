using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class sim_brand
    {
        public sim_brand()
        {
            this.product_gps = new List<product_gps>();
        }

        public byte Id { get; set; }
        public string BrandName { get; set; }
        public virtual ICollection<product_gps> product_gps { get; set; }
    }
}
