using Event_Calendar_WebApi.Contracts;
using Event_Calendar_WebApi.Data;
using Event_Calendar_WebApi.Models;

namespace Event_Calendar_WebApi.Business
{
    public class UserBusiness
    {
        private IUserData userDataAccess;

        public UserBusiness(DataContext dataContext)
        {
            userDataAccess = new UserData(dataContext);
        }

        public User CreateUser(User user)
        {
            return userDataAccess.CreateUser(user);
        }

        public User UpdateUser(User user)
        {
            var userLocal = GetUser(user.UserId);
            return userDataAccess.UpdateUser(userLocal);
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
