
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.RankingAggregateRoot;
using CodeGo.Domain.RankingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGo.Infrastructure.Persistance.Configurations;

public class RankingConfiguration : IEntityTypeConfiguration<Ranking>
{
    public void Configure(EntityTypeBuilder<Ranking> builder)
    {
        ConfigureRankingProgressTable(builder);
        ConfigureRankingTable(builder);
    }

    private static void ConfigureRankingProgressTable(EntityTypeBuilder<Ranking> builder)
    {
        builder.OwnsMany(r => r.RankingProgresses, rpb =>
        {
            rpb.ToTable("rankingProgresses");
            rpb.WithOwner().HasForeignKey("RankingId");
            rpb.HasKey("RankingId", "Id");
            rpb.Property(rp => rp.Id)
                .HasColumnName("RankingProgressId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => RankingProgressId.Create(value));
            rpb.OwnsOne(rp => rp.Points);
            rpb.Property(rp => rp.UserId)
                .HasConversion(
                    userId => userId.Value,
                    value => UserId.Create(value));
            rpb.Property(rp => rp.UserFullName)
                .HasMaxLength(100);
        });
    }

    private static void ConfigureRankingTable(EntityTypeBuilder<Ranking> builder)
    {
        builder.ToTable("rankings");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => RankingId.Create(value));
        builder.OwnsOne(r => r.Period);
        builder.Property(r => r.CourseId)
            .HasConversion(
                courseId => courseId.Value,
                value => CourseId.Create(value));
    }
}
