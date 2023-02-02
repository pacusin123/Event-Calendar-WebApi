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

namespace Event_Calendar_WebApi.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserBusiness userBusiness;

        public UserController(DataContext dataContext)
        {
            userBusiness = new UserBusiness(dataContext);
        }

        [HttpGet]
        [Route("GetUsers")]
        public List<User> GetUsers()
        {
            var users = userBusiness.GetUsers();
            return users.ToList();
        }


        [HttpPost]
        [Route("SaveUser")]
        public User CreateUser([FromBody] User user)
        {
                var userSaved = userBusiness.CreateUser(user);
                return userSaved;
        }

        [HttpPut]
        [Route("UpdateUser")]
        public User UpdateUser([FromBody] User user)
        {
            var userUpdated = userBusiness.UpdateUser(user);
            return userUpdated;
        }


        [HttpDelete]
        [Route("DeleteUser/{id}")]
        public IResult DeleteUser(int id)
        {
            userBusiness.DeleteUser(id);
            return Results.Ok();
        }

        [HttpGet]
        [Route("GetUser/{id}")]
        public User GetUser(int id)
        {
            var user = userBusiness.GetUser(id);
            return user;
        }

        [HttpGet]
        [Route("GetUserByFilter")]
        public List<User> GetUsersByFilter(string filter)
        {
            var users = userBusiness.GetUsersByFilter(filter);
            return users.ToList();
        }


    }
}
