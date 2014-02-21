using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.account;
using gtrackProject.Models.driver;
using gtrackProject.Models.universe;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class ProductGps
    {
        public ProductGps()
        {
            Cameras = new List<ProductCamera>();
            Universes = new List<Universe>();
        }
        [Key]
        public int Id { get; set; }
        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Invalid SIM Number")]
        public string SimNum { get; set; }
        [ForeignKey("SimBrand")]
        public byte? SimBrandId { get; set; }
        [ForeignKey("SimPaymentType")]
        public byte? SimPaymentTypeId { get; set; }
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
        public DateTime? ExpireDate { get; set; }
        public DateTime? LastExtendDate { get; set; }
        [ForeignKey("ProductGpsState")]
        public byte? State { get; set; }
        [ForeignKey("MemoryStatus")]
        public byte? MemoryId { get; set; }



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
        public virtual SimBrand SimBrand { get; set; }
        [JsonIgnore]
        public virtual SimPaymentType SimPaymentType { get; set; }
        [JsonIgnore]
        public virtual ProductProcessState ProductGpsState { get; set; }
        [JsonIgnore]
        public virtual ProductGpsVersion ProductGpsVersion { get; set; }
        [JsonIgnore]
        public virtual ProductGpsMemoryStatus MemoryStatus { get; set; }



        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductCamera> Cameras { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogCardReader> LogCardReaders { get; set; }
    }
}
