using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;

namespace EasyPlan.DataAccess.Repositories
{
    public class RoleRepository : Repository<Right>, IRoleRepository
    {
        public RoleRepository(IDataContext dataContext)
            : base(dataContext)
        {

        }
    }
}
