using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DomainModel.Repositories
{
    public interface IMarkRepository : IRepository<Mark>
    {
        Mark FindByItemAndCriterionId(Guid itemId, Guid criterionId);
    }
}
