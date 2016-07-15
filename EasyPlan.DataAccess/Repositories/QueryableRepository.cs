using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;

namespace EasyPlan.DataAccess.Repositories
{
    public class QueryableRepository<T> : IQueryableRepository<T> where T : Entity
    {
        protected readonly IDataContext _dataContext;

        public QueryableRepository(IDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public T Get(Guid id)
        {
            return _dataContext.GetSet<T>().SingleOrDefault(e => e.Id == id);
        }

        public ICollection<T> GetCollection(Expression<Func<T, bool>> predicate = null)
        {
            var _predicate = predicate != null ? predicate : (o => true);

            return _dataContext.GetSet<T>().Where(_predicate).ToList();
        }
    }
}
