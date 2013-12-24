using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class order
    {
        public order()
        {
            this.universes = new List<universe>();
        }

        public int Id { get; set; }
        public int CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> CurrentUser { get; set; }
        public int HeadInstall { get; set; }
        public string HD { get; set; }
        public Nullable<byte> Version { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal FeePerYear { get; set; }
        public string Comment { get; set; }
        public Nullable<byte> Status { get; set; }
        public System.DateTime Deadline { get; set; }
        public Nullable<byte> ExtendType_Id { get; set; }
        public virtual order_extend_type order_extend_type { get; set; }
        public virtual order_status order_status { get; set; }
        public virtual product_gps_version product_gps_version { get; set; }
        public virtual ICollection<universe> universes { get; set; }
    }
}
