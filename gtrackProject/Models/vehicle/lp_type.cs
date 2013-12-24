using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class lp_type
    {
        public lp_type()
        {
            this.vehicles = new List<vehicle>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string Meaning { get; set; }
        public virtual ICollection<vehicle> vehicles { get; set; }
    }
}
