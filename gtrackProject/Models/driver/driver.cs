using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public sealed partial class Driver
    {
        public Driver()
        {
            this.Universes = new List<Universe>();
        }

        public int Id { get; set; }
        public int IdCard { get; set; }
        public short? ExpireCard { get; set; }
        public string TitleName { get; set; }
        public string FirstName { get; set; }
        public string LastNmae { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string Gender { get; set; }
        public int DriverIdCard { get; set; }
        public short ZipCode { get; set; }
        public byte CategoryId { get; set; }
        public int? UserId { get; set; }
        public DriverCategory DriverCategory { get; set; }
        public ICollection<Universe> Universes { get; set; }
    }
}
