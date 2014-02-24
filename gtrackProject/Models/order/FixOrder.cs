using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.account;
using gtrackProject.Models.product;
using gtrackProject.Models.universe;
using Newtonsoft.Json;

namespace gtrackProject.Models.order
{
    public class FixOrder
    {
        public FixOrder()
        {
            Universes = new List<Universe>();
            FixOrders = new List<FixOrder>();
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("CreateByEmployee")]
        public int? CreateBy { get; set; }
        [Required]
        public System.DateTime CreateDate { get; set; }
        [ForeignKey("CurrentUserEmployee")]
        public int? CurrentUser { get; set; }
        [ForeignKey("InstallByEmployee")]
        public int? HeadInstall { get; set; }
        public string Comment { get; set; }
        [ForeignKey("OrderProcessState")]
        public byte? State { get; set; }
        [ForeignKey("FromOrder")]
        public int? FromOrderId { get; set; }
        [ForeignKey("FromFixOrder")]
        public int? FromFixOrderId { get; set; }
        [ForeignKey("ProblemGps")]
        public int ProblemProduct { get; set; }
        [ForeignKey("SolvedGps")]
        public int SolvedProduct { get; set; }


        [JsonIgnore]
        public virtual Employee CreateByEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee CurrentUserEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee InstallByEmployee { get; set; }
        [JsonIgnore]
        public virtual OrderState OrderProcessState { get; set; }
        [JsonIgnore]
        public virtual Order FromOrder { get; set; }
        [JsonIgnore]
        public virtual FixOrder FromFixOrder { get; set; }
        [JsonIgnore]
        public virtual Gps ProblemGps { get; set; }
        [JsonIgnore]
        public virtual Gps SolvedGps { get; set; }
        


        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrder> FixOrders { get; set; }
    }
}
