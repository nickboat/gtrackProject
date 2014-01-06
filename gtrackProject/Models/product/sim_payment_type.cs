using System.Collections.Generic;

namespace gtrackProject.Models.product
{
    public sealed class SimPaymentType
    {
        public SimPaymentType()
        {
            ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        public string PaymentName { get; set; }
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
