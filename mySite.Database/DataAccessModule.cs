using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mySite.DomainModel.Repositories;
using mySite.DataAccess.Repositories;
using mySite.DomainModel.Entities;

namespace mySite.DataAccess
{
    public class DataAccessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DatabaseContext>()
                .As<IDataContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<StudentRepository>()
               .As<IStudentRepository>()
               .As<IQueryableRepository<Student>>();

            base.Load(builder);
        }
    }
}
