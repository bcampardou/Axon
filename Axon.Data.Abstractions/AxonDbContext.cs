using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Axon.Data.Abstractions
{
    public class AxonDbContext : IdentityDbContext<User, Role, string>
    {
        private const string _currentDateSqlFunction = "getdate()"; 
        public DbSet<Project> Projects { get; set; }
        public AxonDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseIdentityColumns().UseHiLo();
            modelBuilder.Entity<User>(b =>
            {
                b.Property(u => u.CreatedAt)
                .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                   .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<Role>(b =>
            {
                b.Property(u => u.CreatedAt)
                .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                   .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<Tenant>(b =>
            {
                b.Property(u => u.CreatedAt)
                .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                   .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<License>(b =>
            {
                b.Property(u => u.CreatedAt)
                .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                   .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<Project>(b =>
            {
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<Server>(b =>
            {
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<Technology>(b =>
            {
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<Network>(b =>
            {
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<ProjectEnvironment>(b =>
            {
                b.HasKey(e => new { e.ProjectId, e.ServerId, e.Name }).IsClustered(true);
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<ProjectTechnology>(b =>
            {
                b.HasKey(e => new { e.ProjectId, e.TechnologyId }).IsClustered(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
        }
    }
}
