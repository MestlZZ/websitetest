using System;
using System.Collections.Generic;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class Criterion : Entity
    {
        public string Title { get; private set; }

        public int Weight { get; private set; }

        public bool IsBenefit { get; private set; }

        public virtual Board Board { get; private set; }

        public virtual ICollection<Mark> Marks { get; private set; }

        public void SetWeight(int weight)
        {
            ArgumentValidation.ThrowIfNull(weight);
            ArgumentValidation.ThrowIfOutOfRange(weight, 0, 20, "criterion weight");

            Weight = weight;
        }
    }
}
