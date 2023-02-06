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
    public class UserLoginBusiness
    {
        private UserBusiness userBusiness;
        private IConfiguration _configuration;
        public UserLoginBusiness(DataContext dataContext, IConfiguration configuration)
        {
            userBusiness = new UserBusiness(dataContext);
            _configuration = configuration;
        }

        public string InitSession(string userName, string Password)
        {
            var user = userBusiness.GetUsers().Where(p => p.UserName == userName && p.Password == Password).FirstOrDefault();
            if (user == null)
                return "";

            var jwt = _configuration.GetSection("Jwt").Get<Jwt>();
            IdentityOptions identityOptions = new IdentityOptions();
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, jwt.Subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("FirstName", user.FirstName),
                new Claim("LastName", user.LastName),
                new Claim("Role", user.Role.Name),
                new Claim(identityOptions.ClaimsIdentity.RoleClaimType, user.Role.Name)
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
