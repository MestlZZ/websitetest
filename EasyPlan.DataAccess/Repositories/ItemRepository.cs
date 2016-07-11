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

            if(title == null || !(title is string) || title.Length > 254 || title.Length == 0)
            {
                throw new ArgumentException("title is invalid");
            }

            item.Title = title;
        }
    }
}
