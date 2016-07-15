using System;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class Mark : Entity
    {
        public Mark() { }

        private Mark(int value)
        {
            SetValue(value);
        }

        protected internal Mark(Item item, int value)
            :this(value)
        {
            ArgumentValidation.ThrowIfNull(item, "item");

            Item = item;
        }

        protected internal Mark(Criterion criterion, int value)
            :this(value)
        {
            ArgumentValidation.ThrowIfNull(criterion, "criterion");

            Criterion = criterion;
        }

        protected internal Mark(Item item, Criterion criterion, int value)
            :this(item, value)
        {
            ArgumentValidation.ThrowIfNull(criterion, "criterion");

            Criterion = criterion;
        }

        public virtual Criterion Criterion { get; private set; }

        public virtual Item Item { get; private set; }

        public Guid CriterionId { get; private set; }

        public Guid ItemId { get; private set; }

        public int Value { get; private set; }

        public void SetValue(int value)
        {
            ArgumentValidation.ThrowIfNull(value, "mark value");
            ArgumentValidation.ThrowIfOutOfRange(value, 0, 5, "mark value");

            Value = value;
        }
    }
}
