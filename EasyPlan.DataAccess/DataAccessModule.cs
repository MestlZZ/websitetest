using Autofac;
using EasyPlan.DomainModel.Repositories;
using EasyPlan.DataAccess.Repositories;
using EasyPlan.DomainModel.Entities;
using EasyPlan.Infrastructure;

namespace EasyPlan.DataAccess
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

            builder.RegisterType<UserRepository>()
               .As<IUserRepository>()
               .As<IQueryableRepository<User>>();

            builder.RegisterType<RoleRepository>()
               .As<IRoleRepository>()
               .As<IQueryableRepository<Role>>();

            builder.RegisterType<UserRoleRepository>()
               .As<IUserRoleRepository>()
               .As<IQueryableRepository<UserRole>>();

            builder.RegisterType<MarkRepository>()
               .As<IMarkRepository>()
               .As<IQueryableRepository<Mark>>();

            builder.RegisterType<ItemRepository>()
               .As<IItemRepository>()
               .As<IQueryableRepository<Item>>();

            builder.RegisterType<CriterionRepository>()
               .As<ICriterionRepository>()
               .As<IQueryableRepository<Criterion>>();

            base.Load(builder);
        }
    }
}
