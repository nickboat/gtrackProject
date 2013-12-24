using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class user_profile
    {
        public user_profile()
        {
            this.product_gps = new List<product_gps>();
            this.product_gps1 = new List<product_gps>();
            this.product_gps2 = new List<product_gps>();
            this.product_gps3 = new List<product_gps>();
            this.product_gps4 = new List<product_gps>();
            this.product_gps5 = new List<product_gps>();
        }

        public short Id { get; set; }
        public string ASP_Id { get; set; }
        public short HD_Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Fullname { get; set; }
        public string Company_Name { get; set; }
        public virtual hd hd { get; set; }
        public virtual ICollection<product_gps> product_gps { get; set; }
        public virtual ICollection<product_gps> product_gps1 { get; set; }
        public virtual ICollection<product_gps> product_gps2 { get; set; }
        public virtual ICollection<product_gps> product_gps3 { get; set; }
        public virtual ICollection<product_gps> product_gps4 { get; set; }
        public virtual ICollection<product_gps> product_gps5 { get; set; }
    }
}
