using System;
using System.Collections.Generic;
using gtrackProject.Models.order;

namespace gtrackProject.Models
{
    public sealed partial class OrderStatus
    {
        public OrderStatus()
        {
            this.FixOrders = new List<FixOrders>();
            this.Orders = new List<Order>();
        }

        public byte Id { get; set; }
        public string StatusTh { get; set; }
        public string StatusEn { get; set; }
        public ICollection<FixOrders> FixOrders { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
