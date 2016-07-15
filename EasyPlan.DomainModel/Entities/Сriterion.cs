using System;
using System.Collections.Generic;

namespace EasyPlan.DomainModel.Entities
{
    public class Criterion : Entity
    {
        public string Title { get; private set; }

        public int Weight { get; private set; }

        public bool IsBenefit { get; private set; }

        public virtual Board Board { get; private set; }

        public virtual ICollection<Mark> Marks { get; private set; }
    }
}
