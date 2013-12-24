using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class un_display_status
    {
        public un_display_status()
        {
            this.universes = new List<universe>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<universe> universes { get; set; }
    }
}
