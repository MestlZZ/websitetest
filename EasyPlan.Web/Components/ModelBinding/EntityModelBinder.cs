using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using EasyPlan.DataAccess.Repositories;
using EasyPlan.DomainModel.Entities;
using EasyPlan.Infrastructure;
using EasyPlan.DomainModel.Repositories;

namespace EasyPlan.Web.Components.ModelBinding
{
    public interface IEntityModelBinder<T> : IModelBinder { }

    public class EntityModelBinder<T> : IEntityModelBinder<T> where T : Entity
    {
        private readonly IQueryableRepository<T> _repository;

        public EntityModelBinder()
        {
            _repository = DependencyResolver.Current.GetService<IQueryableRepository<T>>();
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var entityId = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "Id");

            Guid id;

            try
            {
                id = Guid.Parse(entityId.AttemptedValue);
            }
            catch
            {
                throw new ArgumentValidationException(bindingContext.ModelName + " id is invalid!", statusCode: 400);
            }

            ArgumentValidation.ThrowIfNull(id, bindingContext.ModelName + " id");

            var model = _repository.Get(id);

            if (model == null)
                throw new ArgumentValidationException(bindingContext.ModelName + " not found!", statusCode: 404);

            return model;       
        }
    }
}