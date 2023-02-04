using Event_Calendar_WebApi.Models;

namespace Event_Calendar_WebApi.Contracts
{
    public interface IScheduleEventData
    {
        ScheduleEvent CreateScheduleEvent(ScheduleEvent scheduleEvent);
        ScheduleEvent UpdateScheduleEvent(ScheduleEvent scheduleEvent);
        void DeleteScheduleEvent(ScheduleEvent scheduleEvent);
        ScheduleEvent GetScheduleEvent(int scheduleEventId);
        List<ScheduleEvent> GetScheduleEvents();
        List<ScheduleEvent> GetScheduleEventsByFilter(string filter);
    }
}
