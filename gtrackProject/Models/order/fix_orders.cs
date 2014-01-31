using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using gtrackProject.Models.account;
using gtrackProject.Models.universe;
using Newtonsoft.Json;

namespace gtrackProject.Models.order
{
    public sealed class FixOrders
    {
        public FixOrders()
        {
            Universes = new List<Universe>();
        }

        public int Id { get; set; }
        public int? CreateBy { get; set; }
        [Required] public System.DateTime CreateDate { get; set; }
        public int? CurrentUser { get; set; }
        public int? HeadInstall { get; set; }
        public string Comment { get; set; }
        public byte? State { get; set; }
        public Employee CreateByEmployee { get; set; }
        public Employee CurrentUsermployee { get; set; }
        public OrderProcessState OrderProcessState { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public ICollection<Universe> Universes { get; set; }
    }
}
