using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DomainModel.Repositories
{    
    public interface IRepository<T> : IQueryableRepository<T> where T : Entity
    {
        void Add(T entity);
        void Remove(T entity);
    }
}
