using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Event_Calendar_WebApi.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        public int UserId { get; set; }
        [JsonIgnore]
        public User? User { get; set; }
        public List<ScheduleEvent>? ScheduleEvents { get; set; }
    }
}
