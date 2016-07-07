using System;
using System.Collections.Generic;

namespace mySite.DomainModel.Entities
{
    public class Criterion : Entity
    {
        public string Title { get; set; }

        public int Width { get; set; }

        public bool IsBenefit { get; set; }

        public virtual Board Board { get; set; }

        public Guid BoardId { get; set; }

        public virtual List<Mark> Marks { get; set; }
    }
}
