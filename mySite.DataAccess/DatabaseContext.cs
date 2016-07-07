using System;
using mySite.DomainModel.Entities;
using mySite.DataAccess.Migrations;
using mySite.Infrastructure;
using System.Data.Entity;

namespace mySite.DataAccess
{
    public class DatabaseContext : DbContext, IDataContext, IUnitOfWork
    {
        public DatabaseContext()
        {
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());
        }

        public DbSet<Board> Boards { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Criterion> Criterions { get; set; }
        public DbSet<Mark> Marks { get; set; }

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
            modelBuilder.Properties<Guid>().Where(p => p.Name == "Title").Configure(p => p.HasMaxLength(254).IsRequired());

            modelBuilder.Entity<Mark>().HasRequired(e => e.Criterion).WithMany(e => e.Marks);
            modelBuilder.Entity<Mark>().HasRequired(e => e.Point).WithMany(e => e.Marks);
            modelBuilder.Entity<Mark>().Property(e => e.Value).IsRequired();
            modelBuilder.Entity<Mark>().HasKey(e => new { e.PointId, e.CriterionId });

            modelBuilder.Entity<Point>()
                .HasMany(mark => mark.Marks).WithRequired(e => e.Point);
            modelBuilder.Entity<Point>().HasRequired(e => e.Board).WithMany(e => e.Points);

            modelBuilder.Entity<Criterion>()
                .HasMany(mark => mark.Marks).WithRequired(e => e.Criterion);
            modelBuilder.Entity<Criterion>().HasRequired(e => e.Board).WithMany(e => e.Criterions);

            base.OnModelCreating(modelBuilder);
        }
    }
}
