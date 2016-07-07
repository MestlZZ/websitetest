using mySite.DomainModel.Entities;
using mySite.DomainModel.Repositories;
using System;
using System.Collections.Generic;

namespace mySite.DataAccess.Repositories
{
    public class PointRepository : Repository<Point>, IPointRepository
    {
        public PointRepository(IDataContext dataContext)
            : base(dataContext) { }

        public void SetTitle(string title, string id)
        {
            var item = Get(Guid.Parse(id));
            item.Title = title;
        }
    }
}
