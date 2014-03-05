using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using gtrackProject.Models.account;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class LogFee
    {
        [Key]
        public int Id { get; set; }
        public decimal FeePerYear { get; set; }
        public byte Year { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpireDate { get; set; }
        [Required]
        [ForeignKey("Gps")]
        public int GpsId { get; set; }
        [Required]
        [ForeignKey("CreateByEmployee")]
        public int CreateBy { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }


        [JsonIgnore]
        public virtual Employee CreateByEmployee { get; set; }
        [JsonIgnore]
        public virtual Gps Gps { get; set; }
    }
}