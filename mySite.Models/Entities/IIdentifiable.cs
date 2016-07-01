using System;

namespace mySite.DomainModel.Entities
{
    public interface IIdentifiable
    {
        Guid Id { get; }
    }
}
