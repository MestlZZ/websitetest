using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using mySite.DomainModel.Entities;

namespace mySite.DomainModel.Repositories
{
    public interface IQueryableRepository<T> where T : Identifiable
    {
        T Get(Guid id);
        
        ICollection<T> GetCollection(Expression<Func<T, bool>> predicate = null);
    }
}
