using Event_Calendar_WebApi.Models;

namespace Event_Calendar_WebApi.Contracts
{
    public interface IScheduleData
    {
        Schedule CreateSchedule(Schedule schedule);
        Schedule UpdateSchedule(Schedule schedule);
        void DeleteSchedule(Schedule schedule);
        Schedule GetSchedule(int scheduleId);
        List<Schedule> GetSchedules();
        List<Schedule> GetSchedulesByFilter(string filter);
    }
}
