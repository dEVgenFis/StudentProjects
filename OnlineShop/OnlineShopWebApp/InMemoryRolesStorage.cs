using OnlineShopWebApp.Areas.Models;
using System.Collections.Generic;

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
            foreach (var role in roles)
            {
                if (role.Name == name)
                {
                    return role;
                }
            }
            return null;
        }
        public void AddRole(Role role)
        {
            roles.Add(role);
        }
        public void RemoveRole(string name)
        {
            foreach (var role in roles)
            {
                if (role.Name == name)
                {
                    roles.Remove(role);
                    break;
                }
            }
        }
    }
}