using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class un_cm_engine
    {
        public un_cm_engine()
        {
            this.universes = new List<universe>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Msg_TH { get; set; }
        public string Msg_EN { get; set; }
        public virtual ICollection<universe> universes { get; set; }
    }
}
