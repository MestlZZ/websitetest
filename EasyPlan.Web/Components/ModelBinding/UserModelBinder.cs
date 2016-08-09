using System;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using EasyPlan.DomainModel.Entities;
using EasyPlan.Infrastructure;
using EasyPlan.DomainModel.Repositories;

namespace EasyPlan.Web.Components.ModelBinding
{
    public interface IUserModelBinder : IModelBinder { }

    public class UserModelBinder : IUserModelBinder  
    {
        private readonly IQueryableRepository<User> _repository;

        public UserModelBinder()
        {
            _repository = DependencyResolver.Current.GetService<IQueryableRepository<User>>();
        }

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var userEmail = bindingContext.ValueProvider.GetValue("email");

            var user = (_repository.GetCollection()).FirstOrDefault(e => e.Email == userEmail.AttemptedValue);

            if (user == null)
                throw new ArgumentValidationException("User not found!", 404);

            return user;
        }
    }
}