using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public interface IEntityMapper
    {
        dynamic Map<T>(T entity) where T : Entity;
        dynamic Map<T>(T entity, string username) where T : Entity;
    }
}
