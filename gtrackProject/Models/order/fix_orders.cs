using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class fix_orders
    {
        public fix_orders()
        {
            this.universes = new List<universe>();
        }

        public int Id { get; set; }
        public Nullable<int> CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> CurrentUser { get; set; }
        public int HeadInstall { get; set; }
        public string Comment { get; set; }
        public short Status { get; set; }
        public virtual ICollection<universe> universes { get; set; }
    }
}
