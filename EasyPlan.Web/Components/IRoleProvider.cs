using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components
{
    public interface IRoleProvider
    {
        void SetUserRole(Board board, User user, string roleName);
        void CreateRole(string roleName);
        bool DeleteRole(string roleName);
        IEnumerable<User> FindUsersInRole(Board board, string roleName);
        IEnumerable<string> GetAllRoles();
        IEnumerable<UserRole> GetUserRolesForUser(User user);
        UserRole GetRoleForUser(Board board, User user);
        bool IsUserInRole(Board board, string username, string roleName);
        void RemoveUserFromRole(Board board, User user, string roleName);
        bool RoleExists(string roleName);
        Role FindRoleByName(string roleName);
    }
}
