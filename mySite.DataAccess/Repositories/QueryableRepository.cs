using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using mySite.DomainModel.Entities;
using mySite.DomainModel.Repositories;

namespace mySite.DataAccess.Repositories
{
    public class QueryableRepository<T> : IQueryableRepository<T> where T : Identifiable
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

        public ICollection<T> GetCollection()
        {
            return _dataContext.GetSet<T>().ToList();
        }

        public ICollection<T> GetCollection(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.GetSet<T>().Where(predicate).ToList();
        }
    }
}
