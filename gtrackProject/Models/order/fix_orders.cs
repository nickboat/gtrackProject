using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using gtrackProject.Models.account;
using gtrackProject.Models.universe;

namespace gtrackProject.Models.order
{
    public sealed class FixOrders
    {
        public FixOrders()
        {
            Universes = new List<Universe>();
        }

        public int Id { get; set; }
        [Required] public int CreateBy { get; set; }
        [Required] public System.DateTime CreateDate { get; set; }
        public int? CurrentUser { get; set; }
        public int? HeadInstall { get; set; }
        public string Comment { get; set; }
        [Required] public byte Status { get; set; }
        public Employee CreateByEmployee { get; set; }
        public Employee CurrentUsermployee { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public ICollection<Universe> Universes { get; set; }
    }
}
