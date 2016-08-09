using EasyPlan.DomainModel.Entities;
using EasyPlan.Infrastructure;
using System.Web.Security;

namespace EasyPlan.Web.Components.Providers
{
    public class MembershipProvider : IMembershipProvider
    {      
        public void Authorize(string email, bool rememberMe)
        {
            FormsAuthentication.RedirectFromLoginPage(email, rememberMe);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }

        public bool ChangePassword(User user, string oldPassword, string newPassword)
        {
            if(ValidateUser(user, oldPassword))
            {
                user.SetPassword(newPassword);

                return true;
            }

            return false;
        }

        public bool ValidateUser(User user, string password)
        {
            ArgumentValidation.ThrowIfNull(user, argumentName: "user");
            ArgumentValidation.ThrowIfNullOrWhiteSpace(password, argumentName: "user password");

            return Cryptography.VerifyHash(password, user.HashPassword);
        }        
    }
}