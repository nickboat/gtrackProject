using System.Collections.Generic;
using gtrackProject.Models.order;
using gtrackProject.Models.vehicle;

namespace gtrackProject.Models.account
{
    public sealed class Hd
    {
        public Hd()
        {
            HdDownLines = new List<Hd>();
            Orders = new List<Order>();
            Vehicles = new List<Vehicle>();
            Customers = new List<Customer>();
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
        public ICollection<Customer> Customers { get; set; }

    }
}
