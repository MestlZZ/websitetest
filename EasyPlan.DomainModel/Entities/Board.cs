using System.Collections.Generic;
using System.Linq;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class Board : Entity
    {
        protected internal Board() { }

        public Board(User user, string title = "New board")
        {
            ArgumentValidation.ThrowIfNull(user, argumentName: "user");

            SetTitle(title);

            Items = new List<Item>();
            Criterions = new List<Criterion>();
            Rights = new List<Right>();

            Criterions.Add(new Criterion(this, true));
            Criterions.Add(new Criterion(this, false));

            Rights.Add(new Right(this, user, RoleName.Admin));

            CreatedBy = user.Email;
        }

        public string Title { get; private set; }

        public virtual ICollection<Criterion> Criterions { get; private set; }

        public virtual ICollection<Item> Items { get; private set; }

        public virtual ICollection<Right> Rights { get; private set; }

        public string CreatedBy { get; private set; }

        public void SetTitle(string title)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(title, argumentName: "board title");
            ArgumentValidation.ThrowIfLongerThan(title, 50, argumentName: "board title");

            Title = title;
        }

        public void SetRole(User user, RoleName roleName)
        {
            ArgumentValidation.ThrowIfNull(user, argumentName: "user");
            ArgumentValidation.ThrowIfNull(roleName, argumentName: "role name");

            var role = Rights.FirstOrDefault(e => e.User == user);

            if (role == null)
                Rights.Add(new Right(this, user, roleName));
            else
                role.SetRole(roleName);
        }

        public Right GetRole(User user)
        {
            ArgumentValidation.ThrowIfNull(user, argumentName: "user");

            return Rights.FirstOrDefault(e => e.User == user);
        }

        public bool isUserInRole(User user, RoleName roleName)
        {
            ArgumentValidation.ThrowIfNull(user, argumentName: "user");
            ArgumentValidation.ThrowIfNull(roleName, argumentName: "role name");

            var role = Rights.FirstOrDefault(e => e.User == user);

            return role.Name == roleName;
        }

        public void RemoveUser(User user)
        {
            ArgumentValidation.ThrowIfNull(user, argumentName: "user");

            Rights.Remove(Rights.FirstOrDefault(e => e.User == user));
        }
    }
}
