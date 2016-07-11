using System.Data.Entity;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DataAccess
{
    public interface IDataContext
    {
        IDbSet<T> GetSet<T>() where T : Identifiable;

        void Save();
    }
}
