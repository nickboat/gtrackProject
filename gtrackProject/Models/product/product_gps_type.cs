using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class product_gps_type
    {
        public product_gps_type()
        {
            this.product_gps = new List<product_gps>();
        }

        public byte Id { get; set; }
        public string StatusName_TH { get; set; }
        public string StatusName_EN { get; set; }
        public virtual ICollection<product_gps> product_gps { get; set; }
    }
}
