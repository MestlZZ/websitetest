using System.Collections.Generic;

namespace mySite.DomainModel.Entities
{
    public class Board : Entity
    {
        public string Title { get; set; }

        public virtual List<Criterion> Criterions { get; set; }
        public virtual List<Point> Points { get; set; }
    }
}
