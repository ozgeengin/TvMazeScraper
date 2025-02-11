using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Infastructure.Data.Configurations
{
    public class CastPersonEntityTypeConfiguration : IEntityTypeConfiguration<CastPersonEntity>
    {
        public void Configure(EntityTypeBuilder<CastPersonEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Show)
                .WithMany(x => x.Cast)
                .HasForeignKey(x => x.ShowId)
                .IsRequired();

            builder.HasOne(x => x.Person)
                .WithMany()
                .HasForeignKey(x => x.PersonId)
                .IsRequired();

            builder.HasIndex(x => new { x.ShowId, x.PersonId })
                .IsUnique();
        }
    }
}