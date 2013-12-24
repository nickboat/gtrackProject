using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class vehicle_head_type
    {
        public vehicle_head_type()
        {
            this.vehicle_type = new List<vehicle_type>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<vehicle_type> vehicle_type { get; set; }
    }
}
