using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using System.Collections.Generic;

namespace EasyPlan.DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IDataContext dataContext)
            : base(dataContext)
        {


        }
    }
}
