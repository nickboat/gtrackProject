using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using gtrackProject.Models.order;

namespace gtrackProject.Models.product
{
    public sealed class ProductGpsVersion
    {
        public ProductGpsVersion()
        {
            Orders = new List<Order>();
            ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
