using System;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DomainModel.Repositories
{
    public interface IMarkRepository : IRepository<Mark>
    {
        Mark FindByItemAndCriterionId(Guid itemId, Guid criterionId);
    }
}
