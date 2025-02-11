using Microsoft.EntityFrameworkCore;
using RTL.TvMazeScraper.Domain.Entities;
using RTL.TvMazeScraper.Infastructure.Data.Configurations;

namespace RTL.TvMazeScraper.Infastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<ShowEntity> Shows { get; set; }
        public virtual DbSet<CastPersonEntity> CastPersons { get; set; }
        public virtual DbSet<PersonEntity> Persons { get; set; }

        public ApplicationDbContext() { }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

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
