using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.account;
using gtrackProject.Models.driver;
using gtrackProject.Models.order;
using gtrackProject.Models.universe;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class Gps
    {
        public Gps()
        {
            Universes = new List<Universe>();
            ProblemProductFixOrders = new List<FixOrder>();
            SolvedProductFixOrders = new List<FixOrder>();
            LogFees = new List<LogFee>();
            LogSims = new List<LogSim>();
            LogDeletes=new List<LogDelete>();
            LogMoves=new List<LogMove>();
            LogASwaps=new List<LogSwap>();
            LogBSwaps=new List<LogSwap>();
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("Sim")]
        public int? SimId { get; set; }
        [ForeignKey("Order")]
        public int? OrderId { get; set; }
        [Required]
        public string Serial { get; set; }
        [Required]
        [ForeignKey("ProductGpsVersion")]
        public byte Version { get; set; }
        [ForeignKey("CreateByEmployee")]
        public int? CreateBy { get; set; }
        public DateTime CreateDate { get; set; }
        [ForeignKey("StockByEmployee")]
        public int? StockBy { get; set; }
        public DateTime? StockDate { get; set; }
        [ForeignKey("QcByEmployee")]
        public int? QcBy { get; set; }
        public DateTime? QcDate { get; set; }
        [ForeignKey("InstallByEmployee")]
        public int? InstallBy { get; set; }
        public DateTime? InstallDate { get; set; }
        public string ErrProductComment { get; set; }
        [ForeignKey("BadByEmployee")]
        public int? BadBy { get; set; }
        public DateTime? BadDate { get; set; }
        public string BadComment { get; set; }
        [ForeignKey("UnuseableByEmployee")]
        public int? UnuseableBy { get; set; }
        public DateTime? UnuseableDate { get; set; }
        public string UnuseableComment { get; set; }
        [ForeignKey("ProductGpsState")]
        public byte State { get; set; }
        public bool? HasMem { get; set; }
        public bool? HasCam { get; set; }
        public bool? HasCard { get; set; }
        public bool? InstallByCustomer { get; set; }
        public decimal? Price { get; set; }



        [JsonIgnore]
        public virtual Employee CreateByEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee StockByEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee QcByEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee InstallByEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee BadByEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee UnuseableByEmployee { get; set; }
        [JsonIgnore]
        public virtual GpsState ProductGpsState { get; set; }
        [JsonIgnore]
        public virtual GpsVersion ProductGpsVersion { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; }
        [JsonIgnore]
        public virtual Sim Sim { get; set; }



        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogCardReader> LogCardReaders { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrder> ProblemProductFixOrders { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrder> SolvedProductFixOrders { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogFee> LogFees { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogSim> LogSims { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogDelete> LogDeletes { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogMove> LogMoves { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogSwap> LogASwaps { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogSwap> LogBSwaps { get; set; }
    }
}
