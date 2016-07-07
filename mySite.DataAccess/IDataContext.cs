using System.Data.Entity;
using mySite.DomainModel.Entities;

namespace mySite.DataAccess
{
    public interface IDataContext
    {
        IDbSet<T> GetSet<T>() where T : Identifiable;

        void Save();
    }
}
