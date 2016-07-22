using System.Collections.Generic;
using EasyPlan.Infrastructure;
using System;

namespace EasyPlan.DomainModel.Entities
{
    public class User : Entity
    {
        protected internal User() { }

        public User(string email, string password)
        {
            SetPassword(password);
            SetEmail(email);

            LastLogIn = DateTime.UtcNow;
        }

        public string HashPassword { get; private set; }
        public string Email { get; private set; }
        public DateTime LastLogIn { get; private set; }

        public virtual ICollection<UserRole> UserRoles { get; private set; }

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
    }
}
