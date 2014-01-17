using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using gtrackProject.Models.account;
using gtrackProject.Models.product;
using gtrackProject.Models.universe;

namespace gtrackProject.Models.order
{
    public sealed class Order
    {
        public Order()
        {
            Universes = new List<Universe>();
        }

        public int Id { get; set; }
        [Required] public int CreateBy { get; set; }
        [Required] public System.DateTime CreateDate { get; set; }
        public int? CurrentUser { get; set; }
        public int? HeadInstall { get; set; }
        [Required] public short HdId { get; set; }
        [Required] public byte Version { get; set; }
        [Required] public int Quantity { get; set; }
        [Required] public decimal PricePerUnit { get; set; }
        [Required] public decimal FeePerYear { get; set; }
        public string Comment { get; set; }
        [Required] public byte Status { get; set; }
        [Required] public System.DateTime Deadline { get; set; }
        public byte? ExtendTypeId { get; set; }
        public Employee CreateByEmployee { get; set; }
        public Employee CurrentUserEmployee { get; set; }
        public Hd Hd { get; set; }
        public OrderExtendType OrderExtendType { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ProductGpsVersion ProductGpsVersion { get; set; }
        public ICollection<Universe> Universes { get; set; }
    }
}
