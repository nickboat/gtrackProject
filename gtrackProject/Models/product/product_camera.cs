using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class product_camera
    {
        public int Id { get; set; }
        public string Serial { get; set; }
        public int Product_Id { get; set; }
        public virtual product_gps product_gps { get; set; }
    }
}
