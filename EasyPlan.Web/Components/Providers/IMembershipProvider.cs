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
