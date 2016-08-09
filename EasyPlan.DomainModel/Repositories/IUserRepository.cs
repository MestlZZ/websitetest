using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DomainModel.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User FindUserByEmail(string email);
    }
}
