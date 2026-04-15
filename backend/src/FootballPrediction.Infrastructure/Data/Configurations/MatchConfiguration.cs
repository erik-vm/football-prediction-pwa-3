using FootballPrediction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FootballPrediction.Infrastructure.Data.Configurations;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.HomeTeam).IsRequired().HasMaxLength(100);
        builder.Property(x => x.AwayTeam).IsRequired().HasMaxLength(100);

        builder.HasIndex(x => x.GameWeekId);
        builder.HasIndex(x => x.KickoffTime);
        builder.HasIndex(x => x.IsFinished);
        builder.HasIndex(x => x.ExternalId);

        builder.Ignore(x => x.StageMultiplier);
    }
}
