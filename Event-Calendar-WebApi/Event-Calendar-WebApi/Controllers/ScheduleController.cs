using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Event_Calendar_WebApi.Data;
using Event_Calendar_WebApi.Models;
using Event_Calendar_WebApi.Business;
using Microsoft.AspNetCore.Authorization;

namespace Event_Calendar_WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ScheduleController : Controller
    {
        private readonly ScheduleBusiness scheduleBusiness;

        public ScheduleController(DataContext dataContext)
        {
            scheduleBusiness = new ScheduleBusiness(dataContext);
        }

        [HttpGet]
        [Route("GetSchedules")]
        public List<Schedule> GetSchedules()
        {
            var schedules = scheduleBusiness.GetSchedules();
            return schedules.ToList();
        }

        [HttpPost]
        [Route("SaveSchedule")]
        public Schedule CreateSchedule([FromBody] Schedule schedule)
        {
            var scheduleSaved = scheduleBusiness.CreateSchedule(schedule);
            return scheduleSaved;
        }

        [HttpPut]
        [Route("UpdateSchedule")]
        public Schedule UpdateSchedule([FromBody] Schedule schedule)
        {
            var scheduleUpdated = scheduleBusiness.UpdateSchedule(schedule);
            return scheduleUpdated;
        }


        [HttpDelete]
        [Authorize]
        [Route("DeleteSchedule/{id}")]
        public IResult DeleteSchedule(int id)
        {
            scheduleBusiness.DeleteSchedule(id);
            return Results.Ok();
        }

        [HttpGet]
        [Route("GetSchedule/{id}")]
        public Schedule GetSchedule(int id)
        {
            var schedule = scheduleBusiness.GetSchedule(id);
            return schedule;
        }

        [HttpGet]
        [Route("GetScheduleByUserId/{id}")]
        public Schedule GetScheduleByUserId(int id)
        {
            var schedule = scheduleBusiness.GetScheduleByUserId(id);
            return schedule;
        }

        [HttpGet]
        [Route("VerifyScheduleExist/{id}")]
        public bool VerifyScheduleExist(int id)
        {
            var schedule = scheduleBusiness.VerifyScheduleExist(id);
            return schedule;
        }

        [HttpGet]
        [Route("GetSchedulesByFilter")]
        public List<Schedule> GetSchedulesByFilter(string filter)
        {
            var users = scheduleBusiness.GetSchedulesByFilter(filter);
            return users.ToList();
        }


    }
}
