using System;
using gtrackProject.Models.order;
using gtrackProject.Models.product;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.universe
{
    public class Universe
    {
        public int VehicleId { get; set; }
        public int? GpsProductId { get; set; }
        public DateTime? CurrentDataDatetime { get; set; }
        public int? CorrectDataId { get; set; }
        public DateTime? CorrectDataDatetime { get; set; }
        public string CmCommand { get; set; }
        public string CmEngine { get; set; }
        public string CmMeter { get; set; }
        public string CmBatt { get; set; }
        public decimal? FuelLevel { get; set; }
        public string CmTemp { get; set; }
        public byte? TempLevel { get; set; }
        public string CmGps { get; set; }
        public string CmSignalStatus { get; set; }
        public short? Speed { get; set; }
        public decimal? Direction { get; set; }
        public string IpGps { get; set; }
        public short? Port { get; set; }
        public decimal? LaGoogle { get; set; }
        public decimal? LongGoogle { get; set; }
        public byte DisplayStatus { get; set; }
        public int? DriverId { get; set; }
        public int? OrderId { get; set; }
        public int? FixOrderId { get; set; }
        public int Id { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual FixOrders FixOrders { get; set; }
        public virtual Order Order { get; set; }
        public virtual ProductGps ProductGps { get; set; }
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
