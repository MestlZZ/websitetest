using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using System.Collections.Generic;

namespace EasyPlan.DataAccess.Repositories
{
    public class CriterionRepository : Repository<Criterion>, ICriterionRepository
    {
        public CriterionRepository(IDataContext dataContext)
            : base(dataContext) {
        }
    }
}
