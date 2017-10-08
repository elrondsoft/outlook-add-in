using Helios.Api.Domain.Entities.MainModule;
using Microsoft.EntityFrameworkCore;

namespace Helios.Api.EFContext
{
    public class HeliosDbContext : DbContext
    {
        #region DbSets

        public virtual DbSet<User> Users { get; set; }

        #endregion

        public HeliosDbContext(): base()
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=dealer-advance.database.windows.net;Database=helios;User Id=toto; Password=Qwert12345;");
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
