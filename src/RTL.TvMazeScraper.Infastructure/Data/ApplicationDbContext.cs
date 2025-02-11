using Microsoft.EntityFrameworkCore;
using RTL.TvMazeScraper.Domain.Entities;
using RTL.TvMazeScraper.Infastructure.Data.Configurations;

namespace RTL.TvMazeScraper.Infastructure.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<ShowEntity> Shows { get; set; }
        public DbSet<CastPersonEntity> CastPersons { get; set; }
        public DbSet<PersonEntity> Persons { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShowEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CastPersonEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PersonEntityTypeConfiguration());
        }
    }
}
