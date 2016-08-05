using System.Collections.Generic;
using EasyPlan.Infrastructure;
using System.Linq;

namespace EasyPlan.DomainModel.Entities
{
    public class User : Entity
    {
        protected internal User() { }

        public User(string fullName, string email, string password)
        {
            SetPassword(password);
            SetEmail(email);
            SetFullName(fullName);
        }

        public string HashPassword { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; private set; }

        public virtual ICollection<Role> Roles { get; private set; }

        public void SetPassword(string password)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(password, argumentName: "password");
            ArgumentValidation.ThrowIfLongerThan(password, 255, argumentName: "password");

            HashPassword = Cryptography.GetHash(password);
        }

        public void SetEmail(string email)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(email, argumentName: "Email");
            ArgumentValidation.ThrowIfLongerThan(email, 255, argumentName: "Email");

            Email = email;
        }

        public void SetFullName(string fullName)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(fullName, argumentName: "full Name");
            ArgumentValidation.ThrowIfLongerThan(fullName, 255, argumentName: "full Name");

            FullName = fullName;
        }

        public Role GetRole(Board board)
        {
            ArgumentValidation.ThrowIfNull(board, argumentName: "board");

            return Roles.FirstOrDefault(e => e.Board == board);
        }

        public bool RemoveFromBoard(Board board)
        {
            if(board.CreatedBy == Email)
            {
                return false;
            }
            else
            {
                Roles.Remove(Roles.FirstOrDefault(e => e.Board == board));
                return true;
            }
        }
    }
}
