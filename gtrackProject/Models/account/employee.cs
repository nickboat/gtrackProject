using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using gtrackProject.Models.order;
using gtrackProject.Models.product;
using Newtonsoft.Json;

namespace gtrackProject.Models.account
{
    public class Employee
    {
        public Employee()
        {
            FixCreates = new List<FixOrders>();
            FixCurrents = new List<FixOrders>();
            FixInstalls = new List<FixOrders>();
            OrderCreates = new List<Order>();
            OrderCurrents = new List<Order>();
            OrderInstalls = new List<Order>();
            GpsCreates = new List<ProductGps>();
            GpsStocks = new List<ProductGps>();
            GpsQcs = new List<ProductGps>();
            GpsInstalls = new List<ProductGps>();
            GpsBads = new List<ProductGps>();
            GpsUnuses = new List<ProductGps>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "FullName must be atleast 4 characters")]
        public string FullName { get; set; }
        [Required]
        [RegularExpression(@"(?(^02)^02\d{7}|^0\d{9})$", ErrorMessage = "Invalid Phone Number")]
        public string Phone { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public string AspId { get; set; }
        [RegularExpression(@"^(m|f)$", ErrorMessage = "Please use 'm' = male, 'f' = female")]
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrders> FixCreates { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrders> FixCurrents { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrders> FixInstalls { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Order> OrderCreates { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Order> OrderCurrents { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Order> OrderInstalls { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductGps> GpsCreates { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductGps> GpsStocks { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductGps> GpsQcs { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductGps> GpsInstalls { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductGps> GpsBads { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<ProductGps> GpsUnuses { get; set; }
    }
}
