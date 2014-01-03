using System;
using System.Collections.Generic;
using gtrackProject.Models.account;

namespace gtrackProject.Models
{
    public sealed partial class FixOrders
    {
        public FixOrders()
        {
            this.Universes = new List<Universe>();
        }

        public int Id { get; set; }
        public int? CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public int? CurrentUser { get; set; }
        public int HeadInstall { get; set; }
        public string Comment { get; set; }
        public byte? Status { get; set; }
        public Employee CreateByEmployee { get; set; }
        public Employee CurrentUsermployee { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<Universe> Universes { get; set; }
    }
}
