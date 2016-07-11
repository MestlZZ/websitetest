using System;

namespace EasyPlan.DomainModel.Entities
{
    public class Mark : Entity
    {
        public virtual Criterion Criterion { get; set; }

        public virtual Item Item { get; set; }

        public Guid CriterionId { get; set; }

        public Guid ItemId { get; set; }

        public int Value { get; set; }
    }
}
