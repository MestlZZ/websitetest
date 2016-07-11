using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.Mapper
{
    public abstract class EntityModelMapper<T> : IEntityModelMapper<T> 
        where T : Entity
    {
        public abstract dynamic Map(T entity);

        public virtual dynamic Map(T entity, string username)
        {
            return Map(entity);
        }
    }
}