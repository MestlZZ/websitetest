using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DomainModel.Repositories
{    
    public interface IRepository<T> : IQueryableRepository<T> where T : Identifiable
    {
        void Add(T entity);
        void Remove(T entity);
    }
}
