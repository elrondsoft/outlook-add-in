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
            optionsBuilder.UseSqlServer("Server=mysql.dealer-advance.com;Database=dev_helios_1;User Id=sa-quantumlogic-2; Password=2YAfUFq9ZFsnLAgA;");
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
