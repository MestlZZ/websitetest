﻿using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using EasyPlan.DataAccess;
using EasyPlan.Infrastructure;
using EasyPlan.Web.Components;
using EasyPlan.DomainModel.Entities;
using EasyPlan.Web.Components.Mapper;

namespace EasyPlan.Web
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);
            
            builder.RegisterModule(new DataAccessModule());
            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}