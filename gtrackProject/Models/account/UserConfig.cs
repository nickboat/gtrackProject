using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace gtrackProject.Models.account
{
    public class UserConfig
    {
        [Key]
        public int Id { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public string Asp_Id { get; set; }
        public bool UseHelper { get; set; }
        public bool UseThai { get; set; }
    }
}