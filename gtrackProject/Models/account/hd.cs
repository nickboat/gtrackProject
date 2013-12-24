using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class hd
    {
        public hd()
        {
            this.vehicles = new List<vehicle>();
            this.up_hds = new List<hd>();
            this.user_profile = new List<user_profile>();
        }

        public string Value { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }
        public short Id { get; set; }
        public Nullable<short> HD_Id_upline { get; set; }
        public virtual ICollection<vehicle> vehicles { get; set; }
        public virtual ICollection<hd> up_hds { get; set; }
        public virtual hd up_hd { get; set; }
        public virtual ICollection<user_profile> user_profile { get; set; }
    }
}
