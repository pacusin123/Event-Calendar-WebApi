using Event_Calendar_WebApi.Contracts;
using Event_Calendar_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Calendar_WebApi.Data
{
    public class ScheduleData : IScheduleData
    {
        private readonly DataContext _context;

        public ScheduleData(DataContext context)
        {
            _context = context;
        }
        public Schedule CreateSchedule(Schedule schedule)
        {
            _context.Add(schedule);
            _context.SaveChanges();
            return schedule;
        }

        public void DeleteSchedule(Schedule schedule)
        {
            _context.Remove(schedule);
            _context.SaveChanges();
        }

        public Schedule GetSchedule(int scheduleId)
        {
            return _context.Schedules.Find(scheduleId);
        }

        public List<Schedule> GetSchedules()
        {
            return _context.Schedules.Include(p => p.User).Include(d=> d.ScheduleEvents).ToList();
        }

        public List<Schedule> GetSchedulesByFilter(string filter)
        {
            return _context.Schedules.Where(p => p.Name.Contains(filter)).ToList();
        }

        public Schedule UpdateSchedule(Schedule Schedule)
        {
            _context.Update(Schedule);
            _context.SaveChanges();
            return Schedule;
        }
    }
}
