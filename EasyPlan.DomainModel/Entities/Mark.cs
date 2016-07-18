using System;
using EasyPlan.Infrastructure;

namespace EasyPlan.DomainModel.Entities
{
    public class Mark : Entity
    {
        protected internal Mark() { }

        public Mark(Item item, Criterion criterion, int value)
        {
            SetValue(value);

            ArgumentValidation.ThrowIfNull(criterion, "criterion");
            ArgumentValidation.ThrowIfNull(item, "item");

            Criterion = criterion;
            Item = item;
        }

        public Mark(Guid itemId, Guid criterionId, int value)
        {
            SetValue(value);

            ArgumentValidation.ThrowIfNull(criterionId, "criterion id");
            ArgumentValidation.ThrowIfNull(itemId, "item id");

            CriterionId = criterionId;
            ItemId = itemId;
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
