using mySite.DomainModel.Entities;
using mySite.DomainModel.Repositories;
using System.Collections.Generic;

namespace mySite.DataAccess.Repositories
{
    public class CriterionRepository : Repository<Criterion>, ICriterionRepository
    {
        public CriterionRepository(IDataContext dataContext)
            : base(dataContext) {
        }
    }
}
