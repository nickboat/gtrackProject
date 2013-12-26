using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class SimPaymentType
    {
        public SimPaymentType()
        {
            this.ProductGpss = new List<ProductGps>();
        }

        public byte Id { get; set; }
        public string PaymentName { get; set; }
        public ICollection<ProductGps> ProductGpss { get; set; }
    }
}
