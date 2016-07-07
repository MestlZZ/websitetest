using mySite.DomainModel.Entities;

namespace mySite.Web.Components.Mapper
{
    public interface IEntityMapper
    {
        dynamic Map<T>(T entity) where T : Entity;
        dynamic Map<T>(T entity, string username) where T : Entity;
    }
}
