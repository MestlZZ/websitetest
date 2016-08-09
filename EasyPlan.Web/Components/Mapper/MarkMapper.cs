using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public static class MarkMapper
    {
        public static object Map(Mark entity)
        {
            return new
            {
                id = entity.Id,
                criterionId = entity.CriterionId,
                itemId = entity.ItemId,
                value = entity.Value
            };
        }
    }
}