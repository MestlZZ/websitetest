﻿using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using EasyPlan.DataAccess;
using EasyPlan.Web.Components.Providers;
using EasyPlan.Web.Components.ModelBinding;

namespace EasyPlan.Web
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterGeneric(typeof(EntityModelBinder<>))
                .As(typeof(IEntityModelBinder<>));

            builder.RegisterType(typeof(UserModelBinder))
                .As(typeof(IUserModelBinder));

            builder.RegisterType<MembershipProvider>()
               .As<IMembershipProvider>();

            builder.RegisterModule(new DataAccessModule());
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}