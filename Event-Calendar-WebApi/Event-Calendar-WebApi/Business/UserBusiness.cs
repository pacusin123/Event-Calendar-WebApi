using Event_Calendar_WebApi.Business.Exceptions;
using Event_Calendar_WebApi.Contracts;
using Event_Calendar_WebApi.Data;
using Event_Calendar_WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Event_Calendar_WebApi.Business
{
    public class UserBusiness
    {
        private IUserData userDataAccess;
        private RoleBusiness roleBusiness;
        public UserBusiness(DataContext dataContext)
        {
            userDataAccess = new UserData(dataContext);
            roleBusiness = new RoleBusiness(dataContext);
        }

        public User CreateUser(User user)
        {
            var userLogin = GetUsers().Where(p => p.UserName == user.UserName && p.Password == user.Password).ToList();
            if (userLogin.Count > 0)
                throw new Exception("username already exists, please change your username");
            if(user.RoleId == 0)
            {
                var role = roleBusiness.GetRoleByName("UserLocal");
                if (role == null)
                    throw new RoleException("The role user local no exists, please contact with admin");
                user.RoleId = role.RoleId;
            }
            return userDataAccess.CreateUser(user);
        }

        public User UpdateUser(User user)
        {
            return userDataAccess.UpdateUser(user);
        }

        public void DeleteUser(int userId)
        {
            var user = userDataAccess.GetUser(userId);
            if (user != null)
                userDataAccess.DeleteUser(user);
        }

        public List<User> GetUsers()
        {
            return userDataAccess.GetUsers();
        }
        public List<User> GetUsersByFilter(string filter)
        {
            return userDataAccess.GetUsersByFilter(filter);
        }

        public User GetUser(int userId)
        {
            return userDataAccess.GetUser(userId);
        }
    }
}
