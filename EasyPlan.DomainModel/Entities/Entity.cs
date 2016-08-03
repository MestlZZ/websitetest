using System;

namespace EasyPlan.DomainModel.Entities
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTime.UtcNow;
        }
        
        public DateTime CreatedOn { get; private set; }
        public Guid Id { get; private set; }
    }
}
