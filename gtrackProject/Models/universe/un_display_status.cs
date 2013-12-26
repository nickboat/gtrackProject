using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class UnDisplayStatus
    {
        public UnDisplayStatus()
        {
            this.Universes = new List<Universe>();
        }

        public byte Id { get; set; }
        public string Name { get; set; }
        public ICollection<Universe> Universes { get; set; }
    }
}
