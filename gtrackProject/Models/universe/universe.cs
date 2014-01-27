using System;
using System.ComponentModel.DataAnnotations;
using gtrackProject.Models.driver;
using gtrackProject.Models.order;
using gtrackProject.Models.product;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.universe
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class Universe
    {
        [Required]
        public int VehicleId { get; set; }
        public int? GpsProductId { get; set; }
        public DateTime? CurrentDataDatetime { get; set; }
        public int? CorrectDataId { get; set; }//id in backup
        public DateTime? CorrectDataDatetime { get; set; }
        public string CmCommand { get; set; }
        public string CmEngine { get; set; }
        public string CmMeter { get; set; }
        public string CmBatt { get; set; }
        [Range(0,100)]
        public decimal? FuelLevel { get; set; }
        public string CmTemp { get; set; }
        [Range(0, 140)]
        public byte? TempLevel { get; set; }
        public string CmGps { get; set; }
        public string CmSignalStatus { get; set; }
        [Range(0, 300)]
        public short? Speed { get; set; }
        [Range(0, 359)]
        public decimal? Direction { get; set; }
        public string IpGps { get; set; }
        public short? Port { get; set; }
        public decimal? LaGoogle { get; set; }
        public decimal? LongGoogle { get; set; }
        [Required]
        public byte DisplayStatus { get; set; }
        public int? DriverId { get; set; }
        public int? OrderId { get; set; }
        public int? FixOrderId { get; set; }
        public int Id { get; set; }
        public Driver Driver { get; set; }
        public FixOrders FixOrders { get; set; }
        public Order Order { get; set; }
        public ProductGps ProductGps { get; set; }
        public UnCmBatt UnCmBatt { get; set; }
        public UnCmComm UnCmComm { get; set; }
        public UnCmEngine UnCmEngine { get; set; }
        public UnCmGps UnCmGps { get; set; }
        public UnCmMeter UnCmMeter { get; set; }
        public UnCmSignal UnCmSignal { get; set; }
        public UnCmTemp UnCmTemp { get; set; }
        public UnDisplayStatus UnDisplayStatus { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
