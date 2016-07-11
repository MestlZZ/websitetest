using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using System.Collections.Generic;

namespace EasyPlan.DataAccess.Repositories
{
    public class MarkRepository : Repository<Mark>, IMarkRepository
    {
        public MarkRepository(IDataContext dataContext)
            : base(dataContext)
        { }        
    }
}
