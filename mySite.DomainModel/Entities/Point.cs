using System;
using System.Collections.Generic;

namespace mySite.DomainModel.Entities
{
    public class Point : Entity
    {
        public string Title { get; set; }

        public virtual Board Board { get; set; }

        public Guid BoardId { get; set; }

        public virtual List<Mark> Marks { get; set; }
    }
}
