using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.order;
using gtrackProject.Models.vehicle;
using Newtonsoft.Json;

namespace gtrackProject.Models.account
{
    public class Hd
    {
        public Hd()
        {
            HdDownLines = new List<Hd>();
            Orders = new List<Order>();
            Vehicles = new List<Vehicle>();
            Customers = new List<Customer>();
        }

        public string Value { get; set; }
        [Required]
        [RegularExpression(@"^\d{6}$", ErrorMessage = "Code must be 6 digi")]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
        public string TableName { get; set; }
        [Key]
        public short Id { get; set; }
        [ForeignKey("Upline")]
        public short? HdIdUpline { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Hd> HdDownLines { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public virtual Hd Upline { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Order> Orders { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Vehicle> Vehicles { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<Customer> Customers { get; set; }

    }
}
