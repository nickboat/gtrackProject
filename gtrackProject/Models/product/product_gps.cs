using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class product_gps
    {
        public product_gps()
        {
            this.product_camera = new List<product_camera>();
            this.universes = new List<universe>();
        }

        public int Id { get; set; }
        public string SIM_Num { get; set; }
        public Nullable<byte> SIM_Brand_Id { get; set; }
        public Nullable<byte> SIM_Payment_Type_Id { get; set; }
        public string Serial { get; set; }
        public byte Version { get; set; }
        public short CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<short> StockBy { get; set; }
        public Nullable<System.DateTime> StockDate { get; set; }
        public Nullable<short> QCBy { get; set; }
        public Nullable<System.DateTime> QCDate { get; set; }
        public Nullable<short> InstallBy { get; set; }
        public Nullable<System.DateTime> InstallDate { get; set; }
        public string ErrProductComment { get; set; }
        public Nullable<short> BadBy { get; set; }
        public Nullable<System.DateTime> BadDate { get; set; }
        public string BadComment { get; set; }
        public Nullable<short> UnuseableBy { get; set; }
        public Nullable<System.DateTime> UnuseableDate { get; set; }
        public string UnuseableComment { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public Nullable<System.DateTime> LastExtendDate { get; set; }
        public Nullable<byte> Status_Id { get; set; }
        public virtual ICollection<product_camera> product_camera { get; set; }
        public virtual user_profile user_profile { get; set; }
        public virtual user_profile user_profile1 { get; set; }
        public virtual user_profile user_profile2 { get; set; }
        public virtual user_profile user_profile3 { get; set; }
        public virtual sim_brand sim_brand { get; set; }
        public virtual sim_payment_type sim_payment_type { get; set; }
        public virtual product_gps_type product_gps_type { get; set; }
        public virtual user_profile user_profile4 { get; set; }
        public virtual user_profile user_profile5 { get; set; }
        public virtual product_gps_version product_gps_version { get; set; }
        public virtual ICollection<universe> universes { get; set; }
    }
}
