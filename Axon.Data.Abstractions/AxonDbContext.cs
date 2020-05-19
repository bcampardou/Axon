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
                b.Property(u => u.CreatedAt)
                    .ValueGeneratedOnAdd().HasDefaultValueSql("getdate()");
                b.Property(u => u.EditedAt)
                    .ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("getdate()");
            });
        }
    }
}
