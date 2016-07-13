using EasyPlan.DomainModel.Entities;
using EasyPlan.DomainModel.Repositories;
using System;
using System.Collections.Generic;

namespace EasyPlan.DataAccess.Repositories
{
    public class ItemRepository : Repository<Item>, IItemRepository
    {
        public ItemRepository(IDataContext dataContext)
            : base(dataContext) { }

        public void SetTitle(string title, string id)
        {
            var item = Get(Guid.Parse(id));

            item.Title = title;
        }
    }
}
