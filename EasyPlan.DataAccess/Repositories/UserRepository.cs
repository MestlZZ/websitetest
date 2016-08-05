using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using System.Linq;
using System.Collections.Generic;

namespace EasyPlan.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public User FindUserByEmail(string email)
        {
            var users = GetCollection();

            return users.FirstOrDefault(e => e.Email.Equals(email));
        }
    }
}
