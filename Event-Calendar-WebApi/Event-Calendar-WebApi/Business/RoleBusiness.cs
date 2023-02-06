using Event_Calendar_WebApi.Contracts;
using Event_Calendar_WebApi.Data;
using Event_Calendar_WebApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Event_Calendar_WebApi.Business
{
    public class RoleBusiness
    {
        private IRoleData roleDataAccess;
        public RoleBusiness(DataContext dataContext)
        {
            roleDataAccess = new RoleData(dataContext);
        }

        public Role CreateRole(Role role)
        {
            return roleDataAccess.CreateRole(role);
        }

        public Role UpdateRole(Role role)
        {
            return roleDataAccess.UpdateRole(role);
        }

        public void DeleteRole(int roleId)
        {
            var Role = roleDataAccess.GetRole(roleId);
            if (Role != null)
                roleDataAccess.DeleteRole(Role);
        }

        public List<Role> GetRoles()
        {
            return roleDataAccess.GetRoles();
        }
        public List<Role> GetRolesByFilter(string filter)
        {
            return roleDataAccess.GetRolesByFilter(filter);
        }

        public Role GetRoleByName(string filter)
        {
            return roleDataAccess.GetRoles().Where(p=> p.Name == filter).FirstOrDefault();
        }

        public Role GetRole(int roleId)
        {
            return roleDataAccess.GetRole(roleId);
        }

    }
}
