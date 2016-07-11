using System.Collections.Generic;

namespace EasyPlan.DomainModel.Entities
{
    public class User : Entity
    {
        public List<Board> Boards { get; set; }
    }
}
