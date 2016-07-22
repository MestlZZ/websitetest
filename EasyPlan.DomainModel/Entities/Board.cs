using System.Collections.Generic;
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
            Roles = new List<Role>();

            Criterions.Add(new Criterion(this, true));
            Criterions.Add(new Criterion(this, false));

            Roles.Add(new Role(this, user, RoleName.Admin));
        }

        public string Title { get; private set; }

        public virtual ICollection<Criterion> Criterions { get; private set; }

        public virtual ICollection<Item> Items { get; private set; }

        public virtual ICollection<Role> Roles { get; private set; }

        public void SetTitle(string title)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(title, argumentName: "board title");
            ArgumentValidation.ThrowIfLongerThan(title, 50, argumentName: "board title");

            Title = title;
        }
    }
}
