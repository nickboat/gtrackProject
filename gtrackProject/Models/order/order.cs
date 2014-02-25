using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.account;
using gtrackProject.Models.product;
using gtrackProject.Models.universe;
using Newtonsoft.Json;

namespace gtrackProject.Models.order
{
    public class Order
    {
        public Order()
        {
            Universes = new List<Universe>();
            FixOrders = new List<FixOrder>();
            Gpses = new List<Gps>();
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("CreateByEmployee")]
        public int? CreateBy { get; set; }
        [Required] public DateTime CreateDate { get; set; }
        [ForeignKey("CurrentUserEmployee")]
        public int? CurrentUser { get; set; }
        [ForeignKey("InstallByEmployee")]
        public int? HeadInstall { get; set; }
        [Required]
        [ForeignKey("Hd")]
        public short HdId { get; set; }
        [Required]
        [ForeignKey("ProductGpsVersion")]
        public byte Version { get; set; }
        [Required] public int Quantity { get; set; }
        public string Comment { get; set; }
        [ForeignKey("OrderProcessState")]
        public byte? State { get; set; }
        [Required] public DateTime Deadline { get; set; }



        [JsonIgnore]
        public virtual Employee CreateByEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee CurrentUserEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee InstallByEmployee { get; set; }
        [JsonIgnore]
        public virtual Hd Hd { get; set; }
        [JsonIgnore]
        public virtual OrderState OrderProcessState { get; set; }
        [JsonIgnore]
        public virtual GpsVersion ProductGpsVersion { get; set; }



        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrder> FixOrders { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Gps> Gpses { get; set; }
    }
}
