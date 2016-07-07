using mySite.DomainModel.Entities;
using mySite.DomainModel.Repositories;
using System.Collections.Generic;

namespace mySite.DataAccess.Repositories
{
    public class MarkRepository : Repository<Mark>, IMarkRepository
    {
        public MarkRepository(IDataContext dataContext)
            : base(dataContext)
        { }        
    }
}
