using Microsoft.EntityFrameworkCore;
using RTL.TvMazeScraper.Infastructure.Data.Configurations;
using RTL.TvMazeScraper.Infastructure.Data.Entities;

namespace RTL.TvMazeScraper.Infastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<ShowEntity> Shows { get; set; }
        public virtual DbSet<PersonEntity> People { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShowEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
        }
    }
}
