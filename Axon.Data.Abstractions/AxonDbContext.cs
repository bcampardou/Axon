﻿using System;
using System.Collections.Generic;
using System.Text;
using Axon.Data.Abstractions.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Axon.Data.Abstractions
{
    public class AxonDbContext : IdentityDbContext<User, Role, Guid>
    {
        private const string _currentDateSqlFunction = "now()"; 
        public DbSet<Project> Projects { get; set; }
        public DbSet<Server> Servers { get; set; }
        public DbSet<Network> Networks { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<ProjectTechnology> ProjectTechnologies { get; set; }
        public DbSet<ProjectEnvironment> ProjectEnvironments { get; set; }
        public DbSet<ProjectTeammate> ProjectTeammates { get; set; }
        public DbSet<ServerTeammate> ServersTeammates { get; set; }
        public DbSet<NetworkTeammate> NetworkTeammates { get; set; }
        public DbSet<KnowledgeSheet> KnowledgeSheet { get; set; }

        public AxonDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseHiLo();
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
                b.HasKey(e => new { e.ProjectId, e.ServerId, e.Name });
                b.HasIndex(e => e.Name).IsUnique(true);
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<ProjectTechnology>(b =>
            {
                b.HasKey(e => new { e.ProjectId, e.TechnologyId });
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<ProjectTeammate>(b =>
            {
                b.HasKey(t => new { t.DataId, t.UserId });
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            }); 
            modelBuilder.Entity<ServerTeammate>(b =>
            {
                b.HasKey(t => new { t.DataId, t.UserId });
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            }); 
            modelBuilder.Entity<NetworkTeammate>(b =>
            {
                b.HasKey(t => new { t.DataId, t.UserId });
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<NetworkIntervention>(b =>
            {
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<ServerIntervention>(b =>
            {
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<ProjectIntervention>(b =>
            {
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
            modelBuilder.Entity<KnowledgeSheet>(b =>
            {
                b.Property(u => u.CreatedAt)
                .ValueGeneratedOnAdd().HasDefaultValueSql(_currentDateSqlFunction);
                b.Property(u => u.EditedAt)
                   .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql(_currentDateSqlFunction);
            });
        }
    }
}
