using Event_Calendar_WebApi.Contracts;
using Event_Calendar_WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Event_Calendar_WebApi.Data
{
    public class RoleData : IRoleData
    {
        private readonly DataContext _context;

        public RoleData(DataContext context)
        {
            _context = context;
        }
        public Role CreateRole(Role role)
        {
            _context.Add(role);
            _context.SaveChanges();
            return role;
        }

        public void DeleteRole(Role role)
        {
            _context.Remove(role);
            _context.SaveChanges();
        }

        public Role GetRole(int roleId)
        {
            return _context.Roles.Find(roleId);
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public List<Role> GetRolesByFilter(string filter)
        {
            return _context.Roles.Where(p => p.Name.Contains(filter)).ToList();
        }

        public Role UpdateRole(Role Role)
        {
            _context.Update(Role);
            _context.SaveChanges();
            return Role;
        }
    }
}
