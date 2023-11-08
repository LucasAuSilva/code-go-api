
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.LessonTrackingAggregateRoot;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGo.Infrastructure.Persistance.Configurations;

public class LessonTrackingConfiguration : IEntityTypeConfiguration<LessonTracking>
{
    public void Configure(EntityTypeBuilder<LessonTracking> builder)
    {
        ConfigureLessonTrackingTable(builder);
        ConfigurePracticeTable(builder);
    }

    private static void ConfigurePracticeTable(EntityTypeBuilder<LessonTracking> builder)
    {
        builder.OwnsMany(lt => lt.Practices, pb =>
        {
            pb.ToTable("practices");
            pb.WithOwner().HasForeignKey("LessonTrackingId");
            pb.HasKey("Id", "LessonTrackingId");
            pb.Property(p => p.Id)
                .HasColumnName("PracticeId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => PracticeId.Create(value));
            pb.Property(p => p.ActivityId)
                .HasMaxLength(38);
            pb.Property(p => p.AnswerId)
                .HasMaxLength(38);
            pb.Property(p => p.Type)
                .HasConversion(
                    type => type.Value,
                    value => PracticeType.FromValue(value));
        });
        builder.Metadata.FindNavigation(nameof(LessonTracking.Practices))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureLessonTrackingTable(EntityTypeBuilder<LessonTracking> builder)
    {
        builder.ToTable("lessonTrackings");
        builder.HasKey(lt => lt.Id);
        builder.Property(lt => lt.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => LessonTrackingId.Create(value));
        builder.Property(lt => lt.UserId)
            .HasConversion(
                userId => userId.Value,
                value => UserId.Create(value));
        builder.Property(lt => lt.CourseId)
            .HasConversion(
                courseId => courseId.Value,
                value => CourseId.Create(value));
        builder.Property(lt => lt.ModuleId)
            .HasConversion(
                moduleId => moduleId.Value,
                value => ModuleId.Create(value));
        builder.Property(lt => lt.Status)
            .HasConversion(
                status => status.Value,
                value => LessonStatus.FromValue(value));
    }
}
