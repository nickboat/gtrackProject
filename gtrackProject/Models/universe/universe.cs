using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.driver;
using gtrackProject.Models.order;
using gtrackProject.Models.product;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.universe
{
    /// <summary>
    /// 
    /// </summary>
    public class Universe
    {
        [ForeignKey("Vehicle")]
        public int? VehicleId { get; set; }
        [ForeignKey("ProductGps")]
        public int? GpsProductId { get; set; }
        public DateTime? CurrentDataDatetime { get; set; }
        public int? CorrectDataId { get; set; }//id in backup
        public DateTime? CorrectDataDatetime { get; set; }
        [ForeignKey("UnCmComm")]
        public string CmCommand { get; set; }
        [ForeignKey("UnCmEngine")]
        public string CmEngine { get; set; }
        [ForeignKey("UnCmMeter")]
        public string CmMeter { get; set; }
        [ForeignKey("UnCmBatt")]
        public string CmBatt { get; set; }
        [Range(0,100)]
        public decimal? FuelLevel { get; set; }
        [ForeignKey("UnCmTemp")]
        public string CmTemp { get; set; }
        [Range(0, 140)]
        public byte? TempLevel { get; set; }
        [ForeignKey("UnCmGps")]
        public string CmGps { get; set; }
        [ForeignKey("UnCmSignal")]
        public string CmSignalStatus { get; set; }
        [Range(0, 300)]
        public short? Speed { get; set; }
        [Range(0, 359)]
        public decimal? Direction { get; set; }
        public string IpGps { get; set; }
        public short? Port { get; set; }
        public decimal? LaGoogle { get; set; }
        public decimal? LongGoogle { get; set; }
        [ForeignKey("UnDisplayStatus")]
        public byte? DisplayStatus { get; set; }
        [ForeignKey("Driver")]
        public int? DriverId { get; set; }
        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        [ForeignKey("FixOrder")]
        public int? FixOrderId { get; set; }
        [Key]
        public int Id { get; set; }




        public virtual Driver Driver { get; set; }
        public virtual FixOrder FixOrder { get; set; }
        public virtual Order Order { get; set; }
        public virtual Gps ProductGps { get; set; }
        public virtual UnCmBatt UnCmBatt { get; set; }
        public virtual UnCmComm UnCmComm { get; set; }
        public virtual UnCmEngine UnCmEngine { get; set; }
        public virtual UnCmGps UnCmGps { get; set; }
        public virtual UnCmMeter UnCmMeter { get; set; }
        public virtual UnCmSignal UnCmSignal { get; set; }
        public virtual UnCmTemp UnCmTemp { get; set; }
        public virtual UnDisplayStatus UnDisplayStatus { get; set; }
        public virtual Vehicle Vehicle { get; set; }
    }
}
