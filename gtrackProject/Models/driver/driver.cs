using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class driver
    {
        public driver()
        {
            this.universes = new List<universe>();
        }

        public int Id { get; set; }
        public int IDCard { get; set; }
        public Nullable<short> ExpireCard { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastNmae { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int DriverIDCard { get; set; }
        public short ZIPCode { get; set; }
        public byte Category_Id { get; set; }
        public Nullable<int> User_Id { get; set; }
        public virtual driver_category driver_category { get; set; }
        public virtual ICollection<universe> universes { get; set; }
    }
}
