using Event_Calendar_WebApi.Contracts;
using Event_Calendar_WebApi.Data;
using Event_Calendar_WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Event_Calendar_WebApi.Business
{
    public class ScheduleBusiness
    {
        private IScheduleData scheduleDataAccess;
        public ScheduleBusiness(DataContext dataContext)
        {
            scheduleDataAccess = new ScheduleData(dataContext);
        }

        public Schedule CreateSchedule(Schedule schedule)
        {
            return scheduleDataAccess.CreateSchedule(schedule);
        }

        public Schedule UpdateSchedule(Schedule schedule)
        {
            return scheduleDataAccess.UpdateSchedule(schedule);
        }

        public void DeleteSchedule(int scheduleId)
        {
            var Schedule = scheduleDataAccess.GetSchedule(scheduleId);
            if (Schedule != null)
                scheduleDataAccess.DeleteSchedule(Schedule);
        }

        public List<Schedule> GetSchedules()
        {
            return scheduleDataAccess.GetSchedules();
        }
        public List<Schedule> GetSchedulesByFilter(string filter)
        {
            return scheduleDataAccess.GetSchedulesByFilter(filter);
        }

        public Schedule GetSchedule(int scheduleId)
        {
            return scheduleDataAccess.GetSchedule(scheduleId);
        }

        public Schedule GetScheduleByUserId(int userId)
        {
            var schedule = this.GetSchedules().Where(p => p.UserId == userId).FirstOrDefault();
            return schedule;
        }

        public bool VerifyScheduleExist(int userId)
        {
            var schedule = this.GetSchedules().Where(p => p.UserId == userId).FirstOrDefault();
            return schedule == null ? false : true;
        }


    }
}
