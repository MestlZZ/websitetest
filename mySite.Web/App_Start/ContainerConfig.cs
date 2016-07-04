using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mySite.DomainModel.Repositories;
using mySite.DataAccess;

namespace mySite.Web
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();

            var applicationAssembly = typeof(MvcApplication).Assembly;
            builder.RegisterControllers(applicationAssembly);
            
            builder.RegisterFilterProvider();

            builder.RegisterModule(new DataAccessModule());

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}