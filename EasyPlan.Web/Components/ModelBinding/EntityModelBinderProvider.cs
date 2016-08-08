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

            Type modelBinderType;

            if (typeof(User).IsAssignableFrom(modelType))
            {
                modelBinderType = typeof(IUserModelBinder);
            }
            else
            {
                modelBinderType = typeof(IEntityModelBinder<>).MakeGenericType(modelType);
            }

            return (IModelBinder)DependencyResolver.Current.GetService(modelBinderType);
        }
    }
}