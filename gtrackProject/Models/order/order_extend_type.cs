using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class order_extend_type
    {
        public order_extend_type()
        {
            this.orders = new List<order>();
        }

        public byte Id { get; set; }
        public string TypeName { get; set; }
        public byte Value { get; set; }
        public virtual ICollection<order> orders { get; set; }
    }
}
