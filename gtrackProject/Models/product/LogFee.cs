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
        [Required]
        public decimal FeePerYear { get; set; }
        [Required]
        public byte Year { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
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