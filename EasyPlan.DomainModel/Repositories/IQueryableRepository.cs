using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DomainModel.Repositories
{
    public interface IQueryableRepository<T> where T : Entity
    {
        T Get(Guid id);
        
        ICollection<T> GetCollection(Expression<Func<T, bool>> predicate = null);
    }
}
