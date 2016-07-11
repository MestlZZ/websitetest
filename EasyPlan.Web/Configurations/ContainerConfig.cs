using Autofac;
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

            builder.RegisterType<DependencyResolverWrapper>().As<IDependencyResolverWrapper>();

            builder.RegisterType<BoardMapper>().As<IEntityModelMapper<Board>>();
            builder.RegisterType<MarkMapper>().As<IEntityModelMapper<Mark>>();
            builder.RegisterType<CriterionMapper>().As<IEntityModelMapper<Criterion>>();
            builder.RegisterType<ItemMapper>().As<IEntityModelMapper<Item>>();

            builder.RegisterType<EntityMapper>().As<IEntityMapper>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}