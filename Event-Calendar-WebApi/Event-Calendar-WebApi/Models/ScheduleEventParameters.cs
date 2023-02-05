namespace Event_Calendar_WebApi.Models
{
    public class ScheduleEventParameters
    {
        public int ScheduleId { get; set; }
        public DateTime EventDate { get; set; }
        public bool WithTime { get; set; }
    }
}
