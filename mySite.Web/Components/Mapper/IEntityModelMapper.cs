using mySite.DomainModel.Entities;

namespace mySite.Web.Components.Mapper
{
    public interface IEntityModelMapper<T> where T : Entity
    {
        dynamic Map(T entity);
        dynamic Map(T entity, string username);
    }
}