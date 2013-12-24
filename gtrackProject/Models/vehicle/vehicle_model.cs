using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class vehicle_model
    {
        public vehicle_model()
        {
            this.vehicles = new List<vehicle>();
        }

        public short Id { get; set; }
        public byte Brand_Id { get; set; }
        public string Name { get; set; }
        public Nullable<short> Year { get; set; }
        public byte Type_Id { get; set; }
        public virtual vehicle_brand vehicle_brand { get; set; }
        public virtual ICollection<vehicle> vehicles { get; set; }
        public virtual vehicle_type vehicle_type { get; set; }
    }
}
