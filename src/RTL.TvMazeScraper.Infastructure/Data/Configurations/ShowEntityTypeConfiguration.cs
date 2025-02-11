using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RTL.TvMazeScraper.Domain.Entities;

namespace RTL.TvMazeScraper.Infastructure.Data.Configurations
{
    public class ShowEntityTypeConfiguration : IEntityTypeConfiguration<ShowEntity>
    {
        public void Configure(EntityTypeBuilder<ShowEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(Constants.MaxNameLength)
                .IsRequired();
        }
    }
}