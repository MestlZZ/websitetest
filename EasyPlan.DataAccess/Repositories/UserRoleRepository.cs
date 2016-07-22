using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using System.Collections.Generic;

namespace EasyPlan.DataAccess.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDataContext dataContext)
            : base(dataContext)
        {

        }
    }
}
