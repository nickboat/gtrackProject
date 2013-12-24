using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class vehicle_brand
    {
        public vehicle_brand()
        {
            this.vehicle_model = new List<vehicle_model>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<vehicle_model> vehicle_model { get; set; }
    }
}
