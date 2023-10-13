
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot;
using CodeGo.Domain.ProgressAggregateRoot.Enums;
using CodeGo.Domain.ProgressAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGo.Infrastructure.Persistance.Configurations;

public class ProgressConfiguration : IEntityTypeConfiguration<Progress>
{
    public void Configure(EntityTypeBuilder<Progress> builder)
    {
        ConfigureProgressTable(builder);
        ConfigureCompletedModuleIdsTable(builder);
        ConfigureCompletedSectionIdsTable(builder);
        ConfigureLessonTrackingIdsTable(builder);
        ConfigureModuleTrackingTable(builder);
    }

    private static void ConfigureModuleTrackingTable(EntityTypeBuilder<Progress> builder)
    {
        builder.OwnsMany(p => p.ModuleTrackings, mtb => 
        {
            mtb.ToTable("moduleTrackings");
            mtb.WithOwner().HasForeignKey("ProgressId");
            mtb.HasKey("Id", "ProgressId");
            mtb.Property(mt => mt.Id)
                .HasColumnName("ModuleTrackingId")
                .ValueGeneratedNever()
                .HasConversion(
                    moduleId => moduleId.Value,
                    value => ModuleTrackingId.Create(value));
            mtb.Property(mt => mt.ModuleId)
                .HasConversion(
                    moduleId => moduleId.Value,
                    value => ModuleId.Create(value));
            mtb.Property(mt => mt.LessonsCompleted);
            mtb.Property(mt => mt.Status)
                .HasConversion(
                    status => status.Value,
                    value => ModuleStatus.FromValue(value));
        });
        builder.Metadata.FindNavigation(nameof(Progress.ModuleTrackings))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureLessonTrackingIdsTable(EntityTypeBuilder<Progress> builder)
    {
        builder.OwnsMany(p => p.LessonTrackingIds, ltib =>
        {
            ltib.ToTable("progressLessonTrackingIds");
            ltib.WithOwner().HasForeignKey("ProgressId");
            ltib.HasKey("Id");
            ltib.Property(lti => lti.Value)
                .HasColumnName("LessonTrackingId")
                .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(Progress.LessonTrackingIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureCompletedSectionIdsTable(EntityTypeBuilder<Progress> builder)
    {
        builder.OwnsMany(p => p.CompletedSectionIds, csb =>
        { 
            csb.ToTable("progressCompletedSectionIds");
            csb.WithOwner().HasForeignKey("ProgressId");
            csb.HasKey("Id");
            csb.Property(cm => cm.Value)
                .HasColumnName("CompletedSectionId")
                .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(Progress.CompletedSectionIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureCompletedModuleIdsTable(EntityTypeBuilder<Progress> builder)
    {
        builder.OwnsMany(p => p.CompletedModuleIds, cmb =>
        { 
            cmb.ToTable("progressCompletedModuleIds");
            cmb.WithOwner().HasForeignKey("ProgressId");
            cmb.HasKey("Id");
            cmb.Property(cm => cm.Value)
                .HasColumnName("CompletedModuleId")
                .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(Progress.CompletedModuleIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureProgressTable(EntityTypeBuilder<Progress> builder)
    {
        builder.ToTable("progresses");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ProgressId.Create(value));
        builder.Property(p => p.UserId)
            .ValueGeneratedNever()
            .HasConversion(
                userId => userId.Value,
                value => UserId.Create(value));
        builder.Property(p => p.CourseId)
            .ValueGeneratedNever()
            .HasConversion(
                courseId => courseId.Value,
                value => CourseId.Create(value));
        builder.Property(p => p.CurrentSection)
            .ValueGeneratedNever()
            .HasConversion(
                currentSection => currentSection.Value,
                value => SectionId.Create(value));
    }
}
