using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class UserRole : Entity
    {
        protected internal UserRole() { }

        public UserRole(Board board, User user, Role role)
        {
            ArgumentValidation.ThrowIfNull(board, argumentName: "board");
            ArgumentValidation.ThrowIfNull(user, argumentName: "user");

            SetRole(role);
            Board = board;
            User = user;
        }

        public UserRole(Guid boardId, Guid userId, Role role)
        {
            ArgumentValidation.ThrowIfNull(boardId, argumentName: "board id");
            ArgumentValidation.ThrowIfNull(userId, argumentName: "user id");

            SetRole(role);
            BoardId = boardId;
            UserId = userId;
        }

        public virtual Board Board { get; private set; }

        public virtual User User { get; private set; }

        public Guid BoardId { get; private set; }

        public Guid UserId { get; private set; }

        public virtual Role Role { get; private set; }

        public void SetRole(Role role)
        {
            ArgumentValidation.ThrowIfNull(role, argumentName: "role");

            Role = role;
        }
    }
}
