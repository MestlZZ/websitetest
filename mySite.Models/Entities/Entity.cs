using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySite.DomainModel.Entities
{
    public class Entity : Identifiable
    {
        protected internal Entity()
               : base()
        {
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
        }

        protected internal Entity(string createdBy)
            : this()
        {
            CreatedBy = createdBy;
            ModifiedBy = createdBy;
        }

        protected internal Entity(string createdBy, DateTime createdOn)
            : this(createdBy)
        {
            CreatedOn = createdOn;
            ModifiedOn = createdOn;
        }

        public string CreatedBy { get; protected set; }
        public DateTime CreatedOn { get; protected internal set; }
        public string ModifiedBy { get; protected set; }
        public DateTime ModifiedOn { get; protected internal set; }
    }
}
