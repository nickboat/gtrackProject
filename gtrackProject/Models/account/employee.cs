using System;
using System.Collections.Generic;
using gtrackProject.Models.order;

namespace gtrackProject.Models.account
{
    public class Employee
    {
        public Employee()
        {
            FixCreates = new List<FixOrders>();
            FixCurrents = new List<FixOrders>();
            OrderCreates = new List<Order>();
            OrderCurrents = new List<Order>();
            GpsCreates = new List<ProductGps>();
            GpsStocks = new List<ProductGps>();
            GpsQcs = new List<ProductGps>();
            GpsInstalls = new List<ProductGps>();
            GpsBads = new List<ProductGps>();
            GpsUnuses = new List<ProductGps>();
        }

        public int Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string AspId { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
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
