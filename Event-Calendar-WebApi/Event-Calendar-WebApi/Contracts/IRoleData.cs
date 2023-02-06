using Event_Calendar_WebApi.Models;

namespace Event_Calendar_WebApi.Contracts
{
    public interface IRoleData
    {
        Role CreateRole(Role role);
        Role UpdateRole(Role role);
        void DeleteRole(Role role);
        Role GetRole(int roleId);
        List<Role> GetRoles();
        List<Role> GetRolesByFilter(string filter);
    }
}
