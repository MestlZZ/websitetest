using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;

namespace EasyPlan.DataAccess.Repositories
{
    public class BoardRepository : Repository<Board>, IBoardRepository
    {
        public BoardRepository(IDataContext dataContext)
            : base(dataContext)
        {

        }       
    }
}
