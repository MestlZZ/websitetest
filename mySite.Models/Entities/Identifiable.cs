using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mySite.DomainModel.Entities
{
    public abstract class Identifiable : IIdentifiable
    {
        protected Identifiable()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
