using Event_Calendar_WebApi.Models;

namespace Event_Calendar_WebApi.Contracts
{
    public interface IUserData
    {
        User CreateUser(User user);
        User UpdateUser(User user);
        void DeleteUser(User user);
        User GetUser(int userId);
        List<User> GetUsers();
        List<User> GetUsersByFilter(string filter);
    }
}
