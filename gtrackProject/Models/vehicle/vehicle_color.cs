using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class vehicle_color
    {
        public vehicle_color()
        {
            this.vehicles = new List<vehicle>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<vehicle> vehicles { get; set; }
    }
}
