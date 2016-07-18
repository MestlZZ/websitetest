using System;
using System.Web.Mvc;
using EasyPlan.DomainModel.Entities;

namespace EasyPlan.Web.Components.ModelBinding
{
    public class EntityModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(Type modelType)
        {
            if (!typeof(Entity).IsAssignableFrom(modelType))
                return null;

            var modelBinderType = typeof(IEntityModelBinder<>).MakeGenericType(modelType);
            return (IModelBinder)DependencyResolver.Current.GetService(modelBinderType);
        }
    }
}