using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class Hd
    {
        public Hd()
        {
            this.HdDownLines = new List<Hd>();
            this.Orders = new List<Order>();
            this.Vehicles = new List<Vehicle>();
        }

        public string Value { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string TableName { get; set; }
        public short Id { get; set; }
        public short? HdIdUpline { get; set; }
        public ICollection<Hd> HdDownLines { get; set; }
        public Hd ThisHd { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Vehicle> Vehicles { get; set; }
    }
}
