using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using System.Collections.Generic;
using System;
using System.Linq;

namespace EasyPlan.DataAccess.Repositories
{
    public class MarkRepository : Repository<Mark>, IMarkRepository
    {
        public MarkRepository(IDataContext dataContext)
            : base(dataContext)
        { }        

        public Mark FindByItemAndCriterionId(Guid itemId, Guid criterionId)
        {
            return GetCollection().FirstOrDefault(e => (e.ItemId == itemId && e.CriterionId == criterionId)); 
        }
    }
}
