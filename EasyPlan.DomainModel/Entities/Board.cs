using System.Collections.Generic;

namespace EasyPlan.DomainModel.Entities
{
    public class Board : Entity
    {
        public string Title { get; private set; }

        public virtual ICollection<Criterion> Criterions { get; private set; }

        public virtual ICollection<Item> Items { get; private set; }
    }
}
