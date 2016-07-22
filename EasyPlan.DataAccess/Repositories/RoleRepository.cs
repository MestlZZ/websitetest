using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using System.Collections.Generic;

namespace EasyPlan.DataAccess.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(IDataContext dataContext)
            : base(dataContext)
        {

        }
    }
}
