using System.Collections.Generic;

namespace EasyPlan.DomainModel.Entities
{
    public class Board : Entity
    {
        public string Title { get; set; }

        public virtual List<Criterion> Criterions { get; set; }
        public virtual List<Item> Items { get; set; }
    }
}
