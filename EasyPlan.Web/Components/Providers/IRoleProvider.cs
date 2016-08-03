using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPlan.DomainModel.Entities;
using EasyPlan.Infrastructure;

namespace EasyPlan.Web.Components.Providers
{
    public interface IRoleProvider
    {
        void SetUserRole(Board board, User user, RoleName roleName);
        IEnumerable<User> FindUsersInRole(Board board, RoleName roleName);
        IEnumerable<Role> GetRolesForUser(User user);
        Role GetRoleForUser(Board board, User user);
        bool IsUserInRole(Board board, string email, RoleName roleName);
        void RemoveUserFromRole(Board board, User user);
    }
}
