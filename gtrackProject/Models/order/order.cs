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
        }
        [Key]
        public int Id { get; set; }
        [ForeignKey("CreateByEmployee")]
        public int? CreateBy { get; set; }
        [Required] public System.DateTime CreateDate { get; set; }
        [ForeignKey("CurrentUserEmployee")]
        public int? CurrentUser { get; set; }
        [ForeignKey("InstallByEmployee")]
        public int? HeadInstall { get; set; }
        [Required]
        [ForeignKey("Hd")]
        public short HdId { get; set; }
        [ForeignKey("ProductGpsVersion")]
        public byte? Version { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public decimal PricePerUnit { get; set; }
        [Required] public decimal FeePerYear { get; set; }
        public string Comment { get; set; }
        [ForeignKey("OrderProcessState")]
        public byte? State { get; set; }
        [Required] public System.DateTime Deadline { get; set; }
        [ForeignKey("OrderExtendType")]
        public byte? ExtendTypeId { get; set; }



        [JsonIgnore]
        public virtual Employee CreateByEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee CurrentUserEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee InstallByEmployee { get; set; }
        [JsonIgnore]
        public virtual Hd Hd { get; set; }
        [JsonIgnore]
        public virtual OrderExtendType OrderExtendType { get; set; }
        [JsonIgnore]
        public virtual OrderProcessState OrderProcessState { get; set; }
        [JsonIgnore]
        public virtual ProductGpsVersion ProductGpsVersion { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
    }
}
