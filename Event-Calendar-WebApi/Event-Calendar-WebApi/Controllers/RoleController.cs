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
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly RoleBusiness roleBusiness;

        public RoleController(DataContext dataContext)
        {
            roleBusiness = new RoleBusiness(dataContext);
        }

        [HttpGet]
        [Route("GetRoles")]
        public List<Role> GetRoles()
        {
            var roles = roleBusiness.GetRoles();
            return roles.ToList();
        }

        [HttpPost]
        [Route("SaveRole")]
        public Role CreateRole([FromBody] Role role)
        {
            var roleSaved = roleBusiness.CreateRole(role);
            return roleSaved;
        }

        [HttpPut]
        [Route("UpdateRole")]
        public Role UpdateRole([FromBody] Role role)
        {
            var roleUpdated = roleBusiness.UpdateRole(role);
            return roleUpdated;
        }


        [HttpDelete]
        [Authorize]
        [Route("DeleteRole/{id}")]
        public IResult DeleteRole(int id)
        {
            roleBusiness.DeleteRole(id);
            return Results.Ok();
        }

        [HttpGet]
        [Route("GetRole/{id}")]
        public Role GetRole(int id)
        {
            var role = roleBusiness.GetRole(id);
            return role;
        }

        [HttpGet]
        [Route("GetRolesByFilter")]
        public List<Role> GetRolesByFilter(string filter)
        {
            var users = roleBusiness.GetRolesByFilter(filter);
            return users.ToList();
        }


    }
}
