using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class Employee
    {
        public Employee()
        {
            this.FixCreates = new List<FixOrders>();
            this.FixCurrents = new List<FixOrders>();
            this.OrderCreates = new List<Order>();
            this.OrderCurrents = new List<Order>();
            this.GpsCreates = new List<ProductGps>();
            this.GpsStocks = new List<ProductGps>();
            this.GpsQcs = new List<ProductGps>();
            this.GpsInstalls = new List<ProductGps>();
            this.GpsBads = new List<ProductGps>();
            this.GpsUnuses = new List<ProductGps>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string AspId { get; set; }
        public string Gender { get; set; }
        public System.DateTime BirthDate { get; set; }
        public ICollection<FixOrders> FixCreates { get; set; }
        public ICollection<FixOrders> FixCurrents { get; set; }
        public ICollection<Order> OrderCreates { get; set; }
        public ICollection<Order> OrderCurrents { get; set; }
        public ICollection<ProductGps> GpsCreates { get; set; }
        public ICollection<ProductGps> GpsStocks { get; set; }
        public ICollection<ProductGps> GpsQcs { get; set; }
        public ICollection<ProductGps> GpsInstalls { get; set; }
        public ICollection<ProductGps> GpsBads { get; set; }
        public ICollection<ProductGps> GpsUnuses { get; set; }
    }
}
