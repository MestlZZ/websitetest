using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using mySite.DataAccess;
using mySite.Infrastructure;
using mySite.Infrastructure.Serialization.Providers;
using mySite.Web.Components;
using mySite.DomainModel.Entities;
using mySite.Web.Components.Mapper;

namespace mySite.Web
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
            builder.RegisterType<PointMapper>().As<IEntityModelMapper<Point>>();

            builder.RegisterType<EntityMapper>().As<IEntityMapper>();

            builder.RegisterGeneric(typeof(SerializationProvider<>)).As(typeof(ISerializationProvider<>)).SingleInstance();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}