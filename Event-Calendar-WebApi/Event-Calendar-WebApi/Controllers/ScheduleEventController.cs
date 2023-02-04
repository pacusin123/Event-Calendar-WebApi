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
    [ApiController]
    public class ScheduleEventController : Controller
    {
        private readonly ScheduleEventBusiness scheduleEventBusiness;

        public ScheduleEventController(DataContext dataContext)
        {
            scheduleEventBusiness = new ScheduleEventBusiness(dataContext);
        }

        [HttpGet]
        [Route("GetScheduleEvents")]
        public List<ScheduleEvent> GetScheduleEvents()
        {
            var scheduleEvents = scheduleEventBusiness.GetScheduleEvents();
            return scheduleEvents.ToList();
        }

        [HttpPost]
        [Route("SaveScheduleEvent")]
        public ScheduleEvent CreateScheduleEvent([FromBody] ScheduleEvent scheduleEvent)
        {
            var scheduleEventSaved = scheduleEventBusiness.CreateScheduleEvent(scheduleEvent);
            return scheduleEventSaved;
        }

        [HttpPut]
        [Route("UpdateScheduleEvent")]
        public ScheduleEvent UpdateScheduleEvent([FromBody] ScheduleEvent scheduleEvent)
        {
            var scheduleEventUpdated = scheduleEventBusiness.UpdateScheduleEvent(scheduleEvent);
            return scheduleEventUpdated;
        }


        [HttpDelete]
        [Authorize]
        [Route("DeleteScheduleEvent/{id}")]
        public IResult DeleteScheduleEvent(int id)
        {
            scheduleEventBusiness.DeleteScheduleEvent(id);
            return Results.Ok();
        }

        [HttpGet]
        [Route("GetScheduleEvent/{id}")]
        public ScheduleEvent GetScheduleEvent(int id)
        {
            var scheduleEvent = scheduleEventBusiness.GetScheduleEvent(id);
            return scheduleEvent;
        }

        [HttpGet]
        [Route("GetScheduleEventsByScheduleId/{id}")]
        public List<ScheduleEvent> GetScheduleEventsByScheduleId(int id)
        {
            var scheduleEvent = scheduleEventBusiness.GetScheduleEventsByScheduleId(id);
            return scheduleEvent;
        }

        [HttpGet]
        [Route("getScheduleEventShared/{id}")]
        public List<ScheduleEvent> GetScheduleEventShared(int id)
        {
            var scheduleEvent = scheduleEventBusiness.GetScheduleEventShared(id);
            return scheduleEvent;
        }

        [HttpGet]
        [Route("GetScheduleEventsByFilter")]
        public List<ScheduleEvent> GetScheduleEventsByFilter(string filter)
        {
            var users = scheduleEventBusiness.GetScheduleEventsByFilter(filter);
            return users.ToList();
        }


    }
}
