
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot;
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
        ConfigureCategoryTrackingTable(builder);
        ConfigureLessonTrackingIdsTable(builder);
    }

    private static void ConfigureCategoryTrackingTable(EntityTypeBuilder<Progress> builder)
    {
        builder.OwnsMany(p => p.CategoryTrackings, ctb =>
        {
            ctb.ToTable("categoryTrackings");
            ctb.WithOwner().HasForeignKey("ProgressId");
            ctb.HasKey("Id", "ProgressId");
            ctb.Property(ct => ct.Id)
                .HasColumnName("CategoryTrackingId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => CategoryTrackingId.Create(value));
            ctb.OwnsOne(ct => ct.DifficultyLevel);
            ctb.Property(ct => ct.CategoryId)
                .HasConversion(
                    categoryId => categoryId.Value,
                    value => CategoryId.Create(value));
        });
        builder.Metadata.FindNavigation(nameof(Progress.CategoryTrackings))!
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
        builder.Property(p => p.CurrentModule)
            .ValueGeneratedNever()
            .HasConversion(
                currentModule => currentModule.Value,
                value => ModuleId.Create(value));
        builder.Property(p => p.CurrentSection)
            .ValueGeneratedNever()
            .HasConversion(
                currentSection => currentSection.Value,
                value => SectionId.Create(value));
    }
}
