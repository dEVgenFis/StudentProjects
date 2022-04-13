using OnlineShopWebApp.Areas.Models;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp
{
    public class InMemoryRolesStorage : IRolesStorage
    {
        private List<Role> roles = new List<Role>();
        public List<Role> GetAllRoles()
        {
            return roles;
        }
        public Role TryGetByRoleName(string name)
        {
            return roles.FirstOrDefault(role => role.Name.Equals(name));
        }
        public void AddRole(Role role)
        {
            roles.Add(role);
        }
        public void RemoveRole(Role role)
        {
            roles.Remove(role);
        }
    }
}