using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class order_status
    {
        public order_status()
        {
            this.orders = new List<order>();
        }

        public byte Id { get; set; }
        public string Status_TH { get; set; }
        public string Status_EN { get; set; }
        public virtual ICollection<order> orders { get; set; }
    }
}
