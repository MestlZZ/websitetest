using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public interface IEntityModelMapper<T> where T : Entity
    {
        dynamic Map(T entity);
        dynamic Map(T entity, string username);
    }
}