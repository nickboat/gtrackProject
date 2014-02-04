using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.account;
using gtrackProject.Models.universe;
using Newtonsoft.Json;

namespace gtrackProject.Models.vehicle
{
    public class Vehicle
    {
        public Vehicle()
        {
            Universes = new List<Universe>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string IdCar { get; set; }
        public string LicensePlate { get; set; }
        [ForeignKey("LpType")]
        public byte? LicensePlateType { get; set; }
        [ForeignKey("Province")]
        public byte? LicensePlateAt { get; set; }
        [ForeignKey("VehicleModel")]
        public short? ModelCarId { get; set; }
        [ForeignKey("VehicleColor")]
        public byte? ColorCarId { get; set; }
        [ForeignKey("VehicleOganize")]
        public byte? OganizeCarId { get; set; }
        public string BodyNo { get; set; }
        [Required]
        [ForeignKey("Hd")]
        public short HdId { get; set; }




        public virtual Hd Hd { get; set; }
        public virtual LpType LpType { get; set; }
        public virtual Province Province { get; set; }
        public virtual VehicleColor VehicleColor { get; set; }
        public virtual VehicleModel VehicleModel { get; set; }
        public virtual VehicleOganize VehicleOganize { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
    }
}
