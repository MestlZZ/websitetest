using System;
using System.Collections.Generic;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class Item : Entity
    {
        public Item() { }

        protected internal Item(string title, Board board)
        {
            ArgumentValidation.ThrowIfNull(board, "board");

            SetTitle(title);
            Board = board;
            Marks = new List<Mark>();

            foreach(var criterion in board.Criterions)
            {
                Marks.Add(new Mark(criterion, 0));
            }
        }

        public string Title { get; private set; }

        public virtual Board Board { get; private set; }

        public virtual ICollection<Mark> Marks { get; private set; }

        public void SetTitle(string title)
        {
            ArgumentValidation.ThrowIfNullOrEmpty(title, "item title");
            ArgumentValidation.ThrowIfLongerThan(title, 255, "item title");

            Title = title;
        }
    }
}
