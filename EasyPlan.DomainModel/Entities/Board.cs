using System.Collections.Generic;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class Board : Entity
    {
        protected internal Board() { }

        public Board(User user, Role role, string title = "New board")
        {
            ArgumentValidation.ThrowIfNull(user, argumentName: "user");
            ArgumentValidation.ThrowIfNull(role, argumentName: "role");

            SetTitle(title);

            Items = new List<Item>();
            Criterions = new List<Criterion>();
            UserRoles = new List<UserRole>();

            Criterions.Add(new Criterion(this, true));
            Criterions.Add(new Criterion(this, false));

            UserRoles.Add(new UserRole(this, user, role));
        }

        public string Title { get; private set; }

        public virtual ICollection<Criterion> Criterions { get; private set; }

        public virtual ICollection<Item> Items { get; private set; }

        public virtual ICollection<UserRole> UserRoles { get; private set; }

        public void SetTitle(string title)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(title, argumentName: "board title");
            ArgumentValidation.ThrowIfLongerThan(title, 50, argumentName: "board title");

            Title = title;
        }
    }
}
