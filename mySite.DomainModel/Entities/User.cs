using System.Collections.Generic;

namespace mySite.DomainModel.Entities
{
    public class User : Entity
    {
        public List<Board> Boards { get; set; }
    }
}
