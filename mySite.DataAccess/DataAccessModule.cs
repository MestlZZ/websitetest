using Autofac;
using mySite.DomainModel.Repositories;
using mySite.DataAccess.Repositories;
using mySite.DomainModel.Entities;
using mySite.Infrastructure;

namespace mySite.DataAccess
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>()
                .As<IDataContext>()
                .As<IUnitOfWork>()
                .InstancePerLifetimeScope();

            builder.RegisterType<BoardRepository>()
               .As<IBoardRepository>()
               .As<IQueryableRepository<Board>>();

            builder.RegisterType<MarkRepository>()
               .As<IMarkRepository>()
               .As<IQueryableRepository<Mark>>();

            builder.RegisterType<PointRepository>()
               .As<IPointRepository>()
               .As<IQueryableRepository<Point>>();

            builder.RegisterType<CriterionRepository>()
               .As<ICriterionRepository>()
               .As<IQueryableRepository<Criterion>>();

            base.Load(builder);
        }
    }
}
