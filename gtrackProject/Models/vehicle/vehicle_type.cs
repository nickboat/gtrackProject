using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class vehicle_type
    {
        public vehicle_type()
        {
            this.vehicle_model = new List<vehicle_model>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public byte Head_Id { get; set; }
        public virtual vehicle_head_type vehicle_head_type { get; set; }
        public virtual ICollection<vehicle_model> vehicle_model { get; set; }
    }
}
