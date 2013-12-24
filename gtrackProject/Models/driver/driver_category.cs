using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class driver_category
    {
        public driver_category()
        {
            this.drivers = new List<driver>();
        }

        public byte Id { get; set; }
        public short Value { get; set; }
        public string Name { get; set; }
        public virtual ICollection<driver> drivers { get; set; }
    }
}
