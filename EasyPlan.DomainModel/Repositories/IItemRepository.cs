using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.DomainModel.Repositories
{
    public interface IItemRepository : IRepository<Item>
    {
        void SetTitle(string title, string id);
    }
}
