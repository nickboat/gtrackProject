using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace gtrackProject.Models.product
{
    public class LogStatus
    {
        public LogStatus()
        {
            LogDeletes=new List<LogDelete>();
            LogMoves=new List<LogMoveGps>();
            LogSwaps=new List<LogSwap>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }



        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogDelete> LogDeletes { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogMoveGps> LogMoves { get; set; }
        [JsonIgnore]
        //[IgnoreDataMember]
        public ICollection<LogSwap> LogSwaps { get; set; }
    }
}