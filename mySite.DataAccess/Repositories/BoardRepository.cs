using mySite.DomainModel.Entities;
using mySite.DomainModel.Repositories;
using System.Collections.Generic;

namespace mySite.DataAccess.Repositories
{
    public class BoardRepository : Repository<Board>, IBoardRepository
    {
        public BoardRepository(IDataContext dataContext)
            : base(dataContext)
        {

        }       
    }
}
