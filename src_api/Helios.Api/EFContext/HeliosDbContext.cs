using System;
using System.Diagnostics;
using System.IO;
using Helios.Api.Domain.Entities.MainModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Helios.Api.EFContext
{
    public class HeliosDbContext : DbContext
    {
        #region DbSets
        public virtual DbSet<User> Users { get; set; }
        #endregion

        public HeliosDbContext() : base()
        {}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbServer = "general-mssql-dev.database.windows.net";
            var dbName = "dev_helios";
            var dbLogin = "toto";
            var dbPassword = "489AWLh2yc9NRcaD";

            optionsBuilder.UseSqlServer($"Server={dbServer}; Database={dbName}; User Id={dbLogin}; Password={dbPassword};");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");
                entity.HasKey(c => c.Id);
            });
        }
    }
}
