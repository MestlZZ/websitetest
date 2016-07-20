using System;
using EasyPlan.DomainModel.Entities;
using EasyPlan.DataAccess.Migrations;
using EasyPlan.Infrastructure;
using System.Data.Entity;

namespace EasyPlan.DataAccess
{
    public class DatabaseContext : DbContext, IDataContext, IUnitOfWork
    {
        public DatabaseContext()
            :base("DefaultConnection")
        {
           Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Configuration>());
        }

        public DbSet<Board> Boards { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Criterion> Criterions { get; set; }
        public DbSet<Mark> Marks { get; set; }

        public IDbSet<T> GetSet<T>() where T : Entity
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

            modelBuilder.Entity<Mark>().HasRequired(e => e.Criterion).WithMany(e => e.Marks);
            modelBuilder.Entity<Mark>().HasRequired(e => e.Item).WithMany(e => e.Marks);
            modelBuilder.Entity<Mark>().Property(e => e.Value).IsRequired();
            modelBuilder.Entity<Mark>().HasKey(e => new { e.ItemId, e.CriterionId });

            modelBuilder.Entity<Item>()
                .HasMany(mark => mark.Marks).WithRequired(e => e.Item);
            modelBuilder.Entity<Item>().HasRequired(e => e.Board).WithMany(e => e.Items);

            modelBuilder.Entity<Criterion>()
                .HasMany(mark => mark.Marks).WithRequired(e => e.Criterion);
            modelBuilder.Entity<Criterion>().HasRequired(e => e.Board).WithMany(e => e.Criterions);

            modelBuilder.Entity<Board>().Property(e => e.Title).HasMaxLength(50).IsRequired();

            modelBuilder.Entity<Item>().Property(e => e.Title).HasMaxLength(255).IsRequired();

            modelBuilder.Entity<Criterion>().Property(e => e.Title).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Criterion>().Property(e => e.IsBenefit).IsRequired();
            modelBuilder.Entity<Criterion>().Property(e => e.Weight).IsRequired();

            modelBuilder.Entity<Mark>().Property(e => e.Value).IsRequired();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
