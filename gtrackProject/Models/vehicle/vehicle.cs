using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using gtrackProject.Models.account;
using gtrackProject.Models.universe;
using Newtonsoft.Json;

namespace gtrackProject.Models.vehicle
{
    public sealed class Vehicle
    {
        public Vehicle()
        {
            Universes = new List<Universe>();
        }

        public int Id { get; set; }
        [Required]
        public string IdCar { get; set; }
        public string LicensePlate { get; set; }
        public byte? LicensePlateType { get; set; }
        public byte? LicensePlateAt { get; set; }
        public short? ModelCarId { get; set; }
        public byte? ColorCarId { get; set; }
        public byte? OganizeCarId { get; set; }
        public string BodyNo { get; set; }
        [Required]
        public short HdId { get; set; }
        public Hd Hd { get; set; }
        public LpType LpType { get; set; }
        public Province Province { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
        public VehicleColor VehicleColor { get; set; }
        public VehicleModel VehicleModel { get; set; }
        public VehicleOganize VehicleOganize { get; set; }
    }
}
