using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using mySite.DomainModel.Entities;

namespace mySite.DataAccess
{
    public interface IDataContext
    {
        IDbSet<T> GetSet<T>() where T : Identifiable;
    }
}
