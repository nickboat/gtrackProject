using System;
using System.Collections.Generic;
using gtrackProject.Models.universe;

namespace gtrackProject.Models
{
    public sealed partial class UnCmSignal
    {
        public UnCmSignal()
        {
            this.Universes = new List<Universe>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string MsgTh { get; set; }
        public string MsgEn { get; set; }
        public ICollection<Universe> Universes { get; set; }
    }
}
