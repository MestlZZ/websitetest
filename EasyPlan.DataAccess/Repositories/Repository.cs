using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;

namespace EasyPlan.DataAccess.Repositories
{
    public class Repository<T> : QueryableRepository<T>, IRepository<T> where T : Entity
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
