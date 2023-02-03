using Event_Calendar_WebApi.Contracts;
using Event_Calendar_WebApi.Data;
using Event_Calendar_WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Event_Calendar_WebApi.Business
{
    public class UserBusiness
    {
        private IUserData userDataAccess;
        private IConfiguration _configuration;
        public UserBusiness(DataContext dataContext, IConfiguration configuration)
        {
            userDataAccess = new UserData(dataContext);
            _configuration = configuration;
        }

        public User CreateUser(User user)
        {
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

        public String InitSession(string userName, string Password)
        {
            var user = GetUsers().Where(p => p.UserName == userName && p.Password == Password).FirstOrDefault();
            if (user == null)
                return "";

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                jwt.Issuer,
                jwt.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: signIn
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
