using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using mySite.DomainModel.Entities;
using mySite.DomainModel.Repositories;

namespace mySite.DataAccess.Repositories
{
    public class Repository<T> : QueryableRepository<T>, IRepository<T> where T : Identifiable
    {
        public Repository(IDataContext dataContext)
            : base(dataContext)
        {
        }

        public void Add(T entity)
        {
            _dataContext.GetSet<T>().Add(entity);
        }

        public void Remove(T entity)
        {
            _dataContext.GetSet<T>().Remove(entity);
        }
    }
}
