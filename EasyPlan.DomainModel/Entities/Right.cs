using System;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class Right : Entity
    {
        protected internal Right() { }

        public Right(Board board, User user, RoleName roleName)
        {
            ArgumentValidation.ThrowIfNull(board, argumentName: "board");
            ArgumentValidation.ThrowIfNull(user, argumentName: "user");

            SetRole(roleName);
            Board = board;
            User = user;
        }

        public virtual Board Board { get; private set; }
        
        public virtual User User { get; private set; }

        public Guid BoardId { get; private set; }

        public string UserId { get; private set; }

        public RoleName Name { get; private set; }

        public void SetRole(RoleName roleName)
        {
            ArgumentValidation.ThrowIfNull(roleName, argumentName: "role name");

            Name = roleName;
        }
    }
}
