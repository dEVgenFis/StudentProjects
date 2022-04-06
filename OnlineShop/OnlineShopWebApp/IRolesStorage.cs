using OnlineShopWebApp.Areas.Models;
using System.Collections.Generic;

namespace OnlineShopWebApp
{
    public interface IRolesStorage
    {
        List<Role> GetAllRoles();
        Role TryGetByRoleName(string name);
        void AddRole(Role role);
        void RemoveRole(string name);
    }
}