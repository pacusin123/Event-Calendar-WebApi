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

        public ScheduleEvent GetScheduleEvent(int scheduleEventId)
        {
            return scheduleEventDataAccess.GetScheduleEvent(scheduleEventId);
        }

        public List<ScheduleEvent> GetScheduleEventsByScheduleId(int scheduleId)
        {
            var scheduleEvents = this.GetScheduleEvents().Where(p => p.ScheduleId == scheduleId);
            return scheduleEvents.ToList();
        }

        public List<ScheduleEvent> GetScheduleEventShared(int scheduleId)
        {
            var scheduleEventsSharedAll = this.GetScheduleEvents().Where(p => p.ScheduleId != scheduleId && p.TypeEventEnum == (int)TypeEvent.Share && p.ParentEventId == null).ToList();
            var scheduleEventsWithParentId = this.GetScheduleEvents().Where(p => p.ScheduleId == scheduleId && p.ParentEventId != null).Select(p=> p.ParentEventId);
            foreach (var item in scheduleEventsSharedAll.ToList())
            {
                if (scheduleEventsWithParentId.Any(d => d == item.ScheduleEventId))
                    scheduleEventsSharedAll.Remove(item);
            }
            return scheduleEventsSharedAll;
        }
    }
}
