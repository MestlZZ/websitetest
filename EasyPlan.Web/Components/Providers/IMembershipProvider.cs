using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Providers
{
    public interface IMembershipProvider
    {
        bool ChangePassword(User user, string oldPassword, string newPassword);
        bool ValidateUser(User user, string password);
        void Authorize(string email, bool rememberMe);
        void SignOut();
    }
}
