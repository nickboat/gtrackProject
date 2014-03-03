using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
            FixCreates = new List<FixOrder>();
            FixCurrents = new List<FixOrder>();
            FixInstalls = new List<FixOrder>();
            OrderCreates = new List<Order>();
            OrderCurrents = new List<Order>();
            OrderInstalls = new List<Order>();
            GpsCreates = new List<Gps>();
            GpsStocks = new List<Gps>();
            GpsQcs = new List<Gps>();
            GpsInstalls = new List<Gps>();
            GpsBads = new List<Gps>();
            GpsUnuses = new List<Gps>();
            GpsProblems = new List<Gps>();
            CustCreates = new List<Customer>();
            ThisEmpCreates = new List<Employee>();
            ThisEmpLeaders = new List<Employee>();
            LogFees = new List<LogFee>();
            LogSims = new List<LogSim>();
            LogDeletes=new List<LogDelete>();
            LogMoves=new List<LogMove>();
            LogSwaps=new List<LogSwap>();

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
        public DateTime? GetInDate { get; set; }
        public DateTime? GetOutDate { get; set; }
        public DateTime CreateDate { get; set; }
        [ForeignKey("CreateByEmployee")]
        public int? CreateBy { get; set; }
        [ForeignKey("LeaderEmployee")]
        public int? Leader { get; set; }
        [Required]
        [Range(1000000000000, 9999999999999)]
        public long IdCard { get; set; }

        [JsonIgnore]
        public virtual Employee CreateByEmployee { get; set; }
        [JsonIgnore]
        public virtual Employee LeaderEmployee { get; set; }

        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrder> FixCreates { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrder> FixCurrents { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<FixOrder> FixInstalls { get; set; }
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
        public ICollection<Gps> GpsCreates { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Gps> GpsStocks { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Gps> GpsQcs { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Gps> GpsInstalls { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Gps> GpsBads { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Gps> GpsUnuses { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Gps> GpsProblems { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Customer> CustCreates { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Employee> ThisEmpCreates { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Employee> ThisEmpLeaders { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogFee> LogFees { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogSim> LogSims { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogDelete> LogDeletes { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogMove> LogMoves { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogSwap> LogSwaps { get; set; }
    }
}
