using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class Criterion : Entity
    {
        protected internal Criterion() { }

        public Criterion(Board board, bool isBenefit, string title = "New criteria", int weight = 20)
        {
            ArgumentValidation.ThrowIfNull(board, nameof(board));

            Board = board;
            IsBenefit = isBenefit;

            SetTitle(title);
            SetWeight(weight);

            Marks = new Collection<Mark>();
        }

        public string Title { get; private set; }

        public int Weight { get; private set; }

        public bool IsBenefit { get; private set; }

        public virtual Board Board { get; private set; }

        public virtual ICollection<Mark> Marks { get; private set; }

        public void SetWeight(int weight)
        {
            ArgumentValidation.ThrowIfOutOfRange(weight, 1, 20, "criterion weight");

            Weight = weight;
        }

        public void SetTitle(string title)
        {
            ArgumentValidation.ThrowIfNullOrWhiteSpace(title, "criterion title");
            ArgumentValidation.ThrowIfLongerThan(title, 255, "criterion title");

            Title = title;
        }
    }
}
