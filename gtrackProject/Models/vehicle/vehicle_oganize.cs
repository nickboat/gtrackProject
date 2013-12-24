using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class vehicle_oganize
    {
        public vehicle_oganize()
        {
            this.vehicles = new List<vehicle>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<vehicle> vehicles { get; set; }
    }
}
