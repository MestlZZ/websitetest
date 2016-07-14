using System;
using System.Collections.Generic;

namespace EasyPlan.DomainModel.Entities
{
    public class Item : Entity
    {
        public string Title { get; set; }

        public virtual Board Board { get; set; }

        public virtual List<Mark> Marks { get; set; }
    }
}
