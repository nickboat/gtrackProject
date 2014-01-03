using System;
using System.Collections.Generic;
using gtrackProject.Models.account;

namespace gtrackProject.Models
{
    public sealed partial class Order
    {
        public Order()
        {
            this.Universes = new List<Universe>();
        }

        public int Id { get; set; }
        public int CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int? CurrentUser { get; set; }
        public int HeadInstall { get; set; }
        public short HdId { get; set; }
        public byte? Version { get; set; }
        public int Quantity { get; set; }
        public decimal PricePerUnit { get; set; }
        public decimal FeePerYear { get; set; }
        public string Comment { get; set; }
        public byte? Status { get; set; }
        public System.DateTime Deadline { get; set; }
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
