using System.Text.Json.Serialization;

namespace Event_Calendar_WebApi.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual List<User>? Users { get; set; }
    }
}
