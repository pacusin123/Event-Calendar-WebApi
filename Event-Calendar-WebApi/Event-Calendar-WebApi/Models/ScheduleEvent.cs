using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Event_Calendar_WebApi.Models
{
    public class ScheduleEvent
    {
        public int ScheduleEventId { get; set; }
        public int ScheduleId { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Place { get; set; }
        public int TypeEventEnum { get; set; }
        public int? ParentEventId { get; set; }
        [JsonIgnore]
        public Schedule? Schedule { get; set; }
    }
}
