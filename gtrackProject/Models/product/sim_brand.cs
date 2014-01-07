using System.Collections.Generic;

namespace gtrackProject.Models.product
{
    public sealed class SimBrand
    {
        public SimBrand()
        {
            ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        public string BrandName { get; set; }
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
