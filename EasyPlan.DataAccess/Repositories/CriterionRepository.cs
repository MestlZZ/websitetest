using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;

namespace EasyPlan.DataAccess.Repositories
{
    public class CriterionRepository : Repository<Criterion>, ICriterionRepository
    {
        public CriterionRepository(IDataContext dataContext)
            : base(dataContext) {
        }
    }
}
