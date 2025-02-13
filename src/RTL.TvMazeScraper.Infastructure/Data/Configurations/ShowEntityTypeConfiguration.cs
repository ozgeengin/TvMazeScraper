using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RTL.TvMazeScraper.Infastructure.Data.Entities;

namespace RTL.TvMazeScraper.Infastructure.Data.Configurations
{
    public class ShowEntityTypeConfiguration : IEntityTypeConfiguration<ShowEntity>
    {
        public void Configure(EntityTypeBuilder<ShowEntity> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.TvMazeId)
                .IsUnique();

            builder.Property(x => x.Name)
                .HasMaxLength(Constants.MaxNameLength)
                .IsRequired();

            builder.HasMany(e => e.Cast)
                    .WithMany(e => e.Shows)
                    .UsingEntity("ShowCast",
                        l => l.HasOne(typeof(PersonEntity)).WithMany().HasForeignKey("PersonTvMazeId").HasPrincipalKey(nameof(PersonEntity.TvMazeId)),
                        r => r.HasOne(typeof(ShowEntity)).WithMany().HasForeignKey("ShowTvMazeId").HasPrincipalKey(nameof(ShowEntity.TvMazeId)),
                        j => j.HasKey("PersonTvMazeId", "ShowTvMazeId"));
        }
    }
}