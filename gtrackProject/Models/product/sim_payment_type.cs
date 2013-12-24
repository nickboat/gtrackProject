using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class sim_payment_type
    {
        public sim_payment_type()
        {
            this.product_gps = new List<product_gps>();
        }

        public byte Id { get; set; }
        public string PaymentName { get; set; }
        public virtual ICollection<product_gps> product_gps { get; set; }
    }
}
