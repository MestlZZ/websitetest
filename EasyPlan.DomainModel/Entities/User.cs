using System.Collections.Generic;

namespace EasyPlan.DomainModel.Entities
{
    public class User : Entity
    {
        public ICollection<Board> Boards { get; set; }
    }
}
