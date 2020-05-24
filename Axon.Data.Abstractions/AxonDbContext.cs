using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;
using Microsoft.EntityFrameworkCore;

namespace Axon.Data.Abstractions
{
    public class AxonDbContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public AxonDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseIdentityColumns().UseHiLo();
            modelBuilder.Entity<Project>(b =>
            {
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            });
            modelBuilder.Entity<Server>(b =>
            {
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            });
            modelBuilder.Entity<Technology>(b =>
            {
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            });
            modelBuilder.Entity<Network>(b =>
            {
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            });
            modelBuilder.Entity<ProjectEnvironment>(b =>
            {
                b.HasKey(e => new { e.ProjectId, e.ServerId, e.Name }).IsClustered(true);
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            });
            modelBuilder.Entity<ProjectTechnology>(b =>
            {
                b.HasKey(e => new { e.ProjectId, e.TechnologyId }).IsClustered(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            });
        }
    }
}
