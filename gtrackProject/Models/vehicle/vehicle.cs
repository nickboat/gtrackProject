using System;
using System.Collections.Generic;
using gtrackProject.Models.account;

namespace gtrackProject.Models
{
    public sealed partial class Vehicle
    {
        public Vehicle()
        {
            this.Universes = new List<Universe>();
        }

        public int Id { get; set; }
        public string IdCar { get; set; }
        public string LicensePlate { get; set; }
        public byte? LicensePlateType { get; set; }
        public byte? LicensePlateAt { get; set; }
        public short? ModelCarId { get; set; }
        public byte? ColorCarId { get; set; }
        public byte? OganizeCarId { get; set; }
        public string BodyNo { get; set; }
        public short HdId { get; set; }
        public Hd Hd { get; set; }
        public LpType LpType { get; set; }
        public Province Province { get; set; }
        public ICollection<Universe> Universes { get; set; }
        public VehicleColor VehicleColor { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public VehicleOganize VehicleOganize { get; set; }
    }
}
