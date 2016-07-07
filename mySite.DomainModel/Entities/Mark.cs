using System;

namespace mySite.DomainModel.Entities
{
    public class Mark : Entity
    {
        public virtual Criterion Criterion { get; set; }

        public virtual Point Point { get; set; }

        //public Guid CriterionId { get; set; }

        //public Guid PointId { get; set; }

        public int Value { get; set; }
    }
}
