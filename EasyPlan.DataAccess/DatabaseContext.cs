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
        public DbSet<User> Users { get; set; }
        public DbSet<Right> UserRoles { get; set; }

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
            
            modelBuilder.Entity<Mark>().HasRequired(e => e.Item).WithMany(e => e.Marks);
            modelBuilder.Entity<Mark>().HasRequired(e => e.Criterion).WithMany(e => e.Marks);
            modelBuilder.Entity<Mark>().Property(e => e.Value).IsRequired();
            modelBuilder.Entity<Mark>().HasKey(e => new { e.ItemId, e.CriterionId });
            modelBuilder.Entity<Mark>().Property(e => e.Value).IsRequired();

            modelBuilder.Entity<Item>().HasMany(mark => mark.Marks).WithRequired(e => e.Item);
            modelBuilder.Entity<Item>().HasRequired(e => e.Board).WithMany(e => e.Items);
            modelBuilder.Entity<Item>().Property(e => e.Title).HasMaxLength(255).IsRequired();
            
            modelBuilder.Entity<Criterion>().HasRequired(e => e.Board).WithMany(e => e.Criterions);
            modelBuilder.Entity<Criterion>().HasMany(e => e.Marks).WithRequired(e => e.Criterion);
            modelBuilder.Entity<Criterion>().Property(e => e.Title).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<Criterion>().Property(e => e.IsBenefit).IsRequired();
            modelBuilder.Entity<Criterion>().Property(e => e.Weight).IsRequired();
            
            modelBuilder.Entity<Right>().HasRequired(e => e.Board).WithMany(e => e.Rights).HasForeignKey(e => e.BoardId);
            modelBuilder.Entity<Right>().HasRequired(e => e.User).WithMany(e => e.Rights).HasForeignKey(e => e.UserId);
            modelBuilder.Entity<Right>().Property(e => e.Name).IsRequired();
            modelBuilder.Entity<Right>().HasKey(e => new { e.UserId, e.BoardId });            

            modelBuilder.Entity<Board>().HasMany(e => e.Rights).WithRequired(e => e.Board);
            modelBuilder.Entity<Board>().Property(e => e.Title).HasMaxLength(50).IsRequired();
            modelBuilder.Entity<Board>().HasMany(e => e.Items).WithRequired(e => e.Board);
            modelBuilder.Entity<Board>().HasMany(e => e.Criterions).WithRequired(e => e.Board);

            modelBuilder.Entity<User>().HasMany(e => e.Rights).WithRequired(e => e.User);
            modelBuilder.Entity<User>().HasKey(e => e.Email);
            modelBuilder.Entity<User>().Property(e => e.Email).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.FullName).HasMaxLength(255).IsRequired();
            modelBuilder.Entity<User>().Property(e => e.HashPassword).HasMaxLength(255).IsRequired();            

            base.OnModelCreating(modelBuilder);
        }
    }
}
