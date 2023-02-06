using Event_Calendar_WebApi.Business.Exceptions;
using Event_Calendar_WebApi.Contracts;
using Event_Calendar_WebApi.Data;
using Event_Calendar_WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Event_Calendar_WebApi.Business
{
    public class ScheduleEventBusiness
    {
        private IScheduleEventData scheduleEventDataAccess;
        public ScheduleEventBusiness(DataContext dataContext)
        {
            scheduleEventDataAccess = new ScheduleEventData(dataContext);
        }

        public ScheduleEvent CreateScheduleEvent(ScheduleEvent scheduleEvent)
        {
            var events = GetScheduleEventsByScheduleId(scheduleEvent.ScheduleId);
            if (events.Any(p => p.CreationDate == scheduleEvent.CreationDate && p.TypeEventEnum == (int)TypeEvent.Exclusive && scheduleEvent.TypeEventEnum == (int)TypeEvent.Exclusive))
                throw new ScheduleEventException("Exist other event exclusive with same time, this event cannot be saved");
            var eventsShared = events.Where(p => p.ParentEventId == scheduleEvent.ParentEventId);
            if (scheduleEvent.ParentEventId != null && eventsShared.Count() >= scheduleEvent.Participants)
                throw new Exception("The number of participants has been exceeded, this event cannot be saved in your schedule");
            return scheduleEventDataAccess.CreateScheduleEvent(scheduleEvent);

        }

        public ScheduleEvent UpdateScheduleEvent(ScheduleEvent scheduleEvent)
        {
            return scheduleEventDataAccess.UpdateScheduleEvent(scheduleEvent);
        }

        public void DeleteScheduleEvent(int scheduleEventId)
        {
            var ScheduleEvent = scheduleEventDataAccess.GetScheduleEvent(scheduleEventId);
            if (ScheduleEvent != null)
                scheduleEventDataAccess.DeleteScheduleEvent(ScheduleEvent);
        }

        public List<ScheduleEvent> GetScheduleEvents()
        {
            return scheduleEventDataAccess.GetScheduleEvents();
        }
        public List<ScheduleEvent> GetScheduleEventsByFilter(string filter)
        {
            return scheduleEventDataAccess.GetScheduleEventsByFilter(filter);
        }

        public List<ScheduleEvent> GetScheduleEventsByDate(DateTime eventDate,bool withTime)
        {
            return filterScheduleEventByDate(GetScheduleEvents(), eventDate, withTime);
        }

        public ScheduleEvent GetScheduleEvent(int scheduleEventId)
        {
            return scheduleEventDataAccess.GetScheduleEvent(scheduleEventId);
        }

        public List<ScheduleEvent> GetScheduleEventsByScheduleId(int scheduleId)
        {
            if (scheduleId == 0)
                throw new ScheduleEventException("There is no schedule created, please add an schedule");
            var scheduleEvents = GetScheduleEvents().Where(p => p.ScheduleId == scheduleId);
            return scheduleEvents.ToList();
        }

        public List<ScheduleEvent> GetScheduleEventShared(int scheduleId)
        {
            var scheduleEventsSharedAll = GetScheduleEvents().Where(p => p.ScheduleId != scheduleId && p.TypeEventEnum == (int)TypeEvent.Share && p.ParentEventId == null).ToList();
            var scheduleEventsWithParentId = GetScheduleEvents().Where(p => p.ScheduleId == scheduleId && p.ParentEventId != null).Select(p => p.ParentEventId);
            foreach (var item in scheduleEventsSharedAll.ToList())
            {
                if (scheduleEventsWithParentId.Any(d => d == item.ScheduleEventId))
                    scheduleEventsSharedAll.Remove(item);
            }
            return scheduleEventsSharedAll;
        }

        public List<ScheduleEvent> getScheduleEventsSharedByDate(int scheduleId, DateTime eventDate, bool withTime)
        {
            if (scheduleId == 0)
                throw new ScheduleEventException("There is no schedule created, please add an schedule");
            return filterScheduleEventByDate(GetScheduleEventShared(scheduleId), eventDate, withTime);
        }

        private List<ScheduleEvent> filterScheduleEventByDate(List<ScheduleEvent> scheduleEvents, DateTime eventDate, bool withTime)
        {
            if (withTime)
            {
                return scheduleEvents.Where(p => p.CreationDate == eventDate).ToList();
            }
            return scheduleEvents.Where(p => p.CreationDate.Year == eventDate.Year && p.CreationDate.Month == eventDate.Month && p.CreationDate.Day == eventDate.Day).ToList();
        }
    }
}
