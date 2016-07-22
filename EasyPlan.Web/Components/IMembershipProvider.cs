using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components
{
    public interface IMembershipProvider
    {
        bool ChangePassword(Guid id, string oldPassword, string newPassword);

        User CreateUser(string fullName, string email, string password);

        bool DeleteUser(Guid id);

        User FindUserByEmail(string email);

        bool ValidateUser(Guid id, string password);
        bool ValidateUser(User user, string password);
        bool ValidateUser(string email, string password);
    }
}
