using System;
using System.Collections.Generic;

namespace EasyPlan.DomainModel.Entities
{
    public class Criterion : Entity
    {
        public string Title { get; set; }

        public int Weight { get; set; }

        public bool IsBenefit { get; set; }

        public virtual Board Board { get; set; }

        public virtual List<Mark> Marks { get; set; }
    }
}
