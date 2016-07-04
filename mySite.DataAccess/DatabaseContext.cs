using System;
using System.Data.Entity;
using mySite.DomainModel.Entities;
using mySite.DataAccess.Migrations;

namespace mySite.DataAccess
{
    public class DatabaseContext : DbContext, IDataContext
    {
        public DatabaseContext()
        {
            try
            {
                Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DbSet<Student> Students { get; set; }

        public IDbSet<T> GetSet<T>() where T : Identifiable
        {
            return Set<T>();
        }

        public void Save()
        {
            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<Guid>().Where(p => p.Name == "Id").Configure(p => p.IsKey());
            modelBuilder.Properties<string>().Configure(p => p.IsRequired().HasMaxLength(254));

            modelBuilder.Entity<Student>().Property(p => p.Name).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Student>().Property(p => p.Surname).HasMaxLength(30).IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
