using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class product_gps_version
    {
        public product_gps_version()
        {
            this.orders = new List<order>();
            this.product_gps = new List<product_gps>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<order> orders { get; set; }
        public virtual ICollection<product_gps> product_gps { get; set; }
    }
}
