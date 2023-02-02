using System.ComponentModel.DataAnnotations;

namespace Event_Calendar_WebApi.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(20)]
        public int UserId { get; set; }
        public User User { get; set; }
        public List<ScheduleEvent> ScheduleEvents { get; set; }
    }
}
