using Event_Calendar_WebApi.Contracts;
using Event_Calendar_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Calendar_WebApi.Data
{
    public class ScheduleEventData : IScheduleEventData
    {
        private readonly DataContext _context;

        public ScheduleEventData(DataContext context)
        {
            _context = context;
        }
        public ScheduleEvent CreateScheduleEvent(ScheduleEvent scheduleEvent)
        {
            _context.Add(scheduleEvent);
            _context.SaveChanges();
            return scheduleEvent;
        }

        public void DeleteScheduleEvent(ScheduleEvent scheduleEvent)
        {
            _context.Remove(scheduleEvent);
            _context.SaveChanges();
        }

        public ScheduleEvent GetScheduleEvent(int scheduleEventId)
        {
            return _context.ScheduleEvents.Find(scheduleEventId);
        }

        public List<ScheduleEvent> GetScheduleEvents()
        {
            return _context.ScheduleEvents.Include(p => p.Schedule).ToList();
        }

        public List<ScheduleEvent> GetScheduleEventsByFilter(string filter)
        {
            return _context.ScheduleEvents.Where(p => p.Name.Contains(filter)).ToList();
        }

        public ScheduleEvent UpdateScheduleEvent(ScheduleEvent ScheduleEvent)
        {
            _context.Update(ScheduleEvent);
            _context.SaveChanges();
            return ScheduleEvent;
        }
    }
}
