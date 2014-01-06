using System.Collections.Generic;

namespace gtrackProject.Models.product
{
    public sealed class ProductGpsType
    {
        public ProductGpsType()
        {
            ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        public string StatusNameTh { get; set; }
        public string StatusNameEn { get; set; }
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
