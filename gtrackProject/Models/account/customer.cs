using System;
using System.Collections.Generic;

namespace gtrackProject.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        public string Asp_Id { get; set; }
        public short Hd_Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
    }
}
