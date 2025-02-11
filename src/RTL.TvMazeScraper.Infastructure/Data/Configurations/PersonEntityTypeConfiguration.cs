using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Infastructure.Data.Configurations
{
    public class PersonEntityTypeConfiguration : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(Constants.MaxNameLength)
                .IsRequired();
        }
    }
}