using System.Collections.Generic;

namespace EasyPlan.DomainModel.Entities
{
    public class Board : Entity
    {
        public string Title { get; set; }

        public virtual ICollection<Criterion> Criterions { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
