using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class province
    {
        public province()
        {
            this.vehicles = new List<vehicle>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public string ShortNameEN { get; set; }
        public virtual ICollection<vehicle> vehicles { get; set; }
    }
}
