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
    public class UserLoginController : Controller
    {
        private readonly UserLoginBusiness userLoginBusiness;

        public UserLoginController(DataContext dataContext, IConfiguration configuration)
        {
            userLoginBusiness = new UserLoginBusiness(dataContext, configuration);
        }        

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IActionResult InitSession(string userName, string password)
        {

            var token = userLoginBusiness.InitSession(userName, password);
            if (token == null)
            {
                return BadRequest(new
                {
                    token = "",
                    message = "Credentials incorrects"
                });
            }
            else
            {
                return Ok( new
                {
                    token = token,
                    message = "Login Success"

                });

            }
        }


    }
}
