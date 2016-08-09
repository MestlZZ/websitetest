using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class Item : Entity
    {
        protected internal Item() { }

        public Item(string title, Board board)
        {
            ArgumentValidation.ThrowIfNull(board, "board");

            SetTitle(title);
            Board = board;

            Marks = new Collection<Mark>();
        }

        public string Title { get; private set; }

        public virtual Board Board { get; private set; }

        public virtual ICollection<Mark> Marks { get; private set; }

        public void SetTitle(string title)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(title, "item title");
            ArgumentValidation.ThrowIfLongerThan(title, 255, "item title");

            Title = title;
        }
    }
}
