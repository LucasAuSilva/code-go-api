
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.Entities;
using CodeGo.Domain.CourseAggregateRoot.Enums;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGo.Infrastructure.Persistance.Configurations;

public class CourseConfigurations : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        ConfigureCourseTable(builder);
        ConfigureSectionTable(builder);
        ConfigureQuestionIdsTable(builder);
        ConfigureExerciseIdsTable(builder);
    }

    private static void ConfigureExerciseIdsTable(EntityTypeBuilder<Course> builder)
    {
        builder.OwnsMany(c => c.ExerciseIds, eib =>
        {
            eib.ToTable("courseExerciseIds");
            eib.WithOwner().HasForeignKey("CourseId");
            eib.HasKey("Id");
            eib.Property(ei => ei.Value)
                .HasColumnName("ExerciseId")
                .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(Course.ExerciseIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureQuestionIdsTable(EntityTypeBuilder<Course> builder)
    {
        builder.OwnsMany(c => c.QuestionIds, qib =>
        {
            qib.ToTable("courseQuestionIds");
            qib.WithOwner().HasForeignKey("CourseId");
            qib.HasKey("Id");
            qib.Property(qi => qi.Value)
                .HasColumnName("QuestionId")
                .ValueGeneratedNever();
        });
        builder.Metadata.FindNavigation(nameof(Course.QuestionIds))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureSectionTable(EntityTypeBuilder<Course> builder)
    {
        builder.OwnsMany(c => c.Sections, sb =>
        {
            sb.ToTable("sections");
            sb.WithOwner().HasForeignKey("CourseId");
            sb.HasKey("Id", "CourseId");
            sb.Property(s => s.Id)
                .HasColumnName("SectionId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => SectionId.Create(value));
            sb.Property(s => s.Name)
                .HasMaxLength(100);
            sb.Property(s => s.Description)
                .HasMaxLength(150);
            sb.OwnsMany(s => s.Modules, mb =>
            {
                mb.ToTable("modules");
                mb.WithOwner().HasForeignKey("SectionId", "CourseId");
                mb.HasKey(nameof(Module.Id), "SectionId", "CourseId");
                mb.Property(m => m.Id)
                    .HasColumnName("ModuleId")
                    .ValueGeneratedNever()
                    .HasConversion(
                        id => id.Value,
                        value => ModuleId.Create(value));
                mb.Property(m => m.Name)
                    .HasMaxLength(100);
                mb.Property(m => m.Type)
                    .HasConversion(
                        type => type.Value,
                        value => ModuleType.FromValue(value));
                mb.OwnsOne(m => m.Difficulty);
            });
            sb.Navigation(s => s.Modules).Metadata.SetField("_modules");
            sb.Navigation(s => s.Modules).UsePropertyAccessMode(PropertyAccessMode.Field);
        });
        builder.Metadata.FindNavigation(nameof(Course.Sections))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureCourseTable(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("courses");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CourseId.Create(value));
        builder.Property(c => c.Name)
            .HasMaxLength(100);
        builder.Property(c => c.AuthorName)
            .HasMaxLength(50);
        builder.Property(c => c.Description)
            .HasMaxLength(150);
        builder.Property(c => c.CourseIcon)
            .HasMaxLength(100);
        builder.Property(c => c.Language)
            .HasConversion(
                language => language.Value,
                value => Language.FromValue(value));
    }
}
