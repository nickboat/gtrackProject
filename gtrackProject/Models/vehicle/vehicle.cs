using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class vehicle
    {
        public vehicle()
        {
            this.universes = new List<universe>();
        }

        public int Id { get; set; }
        public string IdCar { get; set; }
        public string LicensePlate { get; set; }
        public Nullable<byte> LicensePlate_Type { get; set; }
        public Nullable<byte> LicensePlate_At { get; set; }
        public Nullable<short> ModelCar_Id { get; set; }
        public Nullable<byte> ColorCar_Id { get; set; }
        public Nullable<byte> OganizeCar_Id { get; set; }
        public string BodyNo { get; set; }
        public short HD_Id { get; set; }
        public virtual hd hd { get; set; }
        public virtual lp_type lp_type { get; set; }
        public virtual province province { get; set; }
        public virtual ICollection<universe> universes { get; set; }
        public virtual vehicle_color vehicle_color { get; set; }
        public virtual vehicle_model vehicle_model { get; set; }
        public virtual vehicle_oganize vehicle_oganize { get; set; }
    }
}
