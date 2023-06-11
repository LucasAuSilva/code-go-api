using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.ExerciseAggregateRoot.Enums;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGo.Infrastructure.Persistance.Configurations;

public class ExerciseConfigurations : IEntityTypeConfiguration<Exercise>
{
    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        ConfigureExerciseTable(builder);
        ConfigureTestCaseTable(builder);
    }

    private static void ConfigureTestCaseTable(EntityTypeBuilder<Exercise> builder)
    {
        builder.OwnsMany(e => e.TestCases, tb =>
        {
            tb.ToTable("testCases");

            tb.WithOwner().HasForeignKey("ExerciseId");

            tb.HasKey("ExerciseId", "Id");
            tb.Property(t => t.Id)
                .HasColumnName("TestCaseId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => TestCaseId.Create(value));
            tb.Property(t => t.Title)
                .HasMaxLength(50);
            tb.Property(t => t.Result)
                .HasColumnType("TEXT");
        });
        builder.Metadata.FindNavigation(nameof(Exercise.TestCases))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureExerciseTable(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("exercises");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => ExerciseId.Create(value));
        builder.OwnsOne(e => e.Difficulty);
        builder.Property(e => e.CourseId)
            .HasConversion(
                id => id.Value,
                value => CourseId.Create(value));
        builder.OwnsOne(e => e.Difficulty);
        builder.Property(e => e.CategoryId)
            .HasConversion(
                id => id.Value,
                value => CategoryId.Create(value));
        builder.Property(e => e.BaseCode)
            .HasColumnType("TEXT");
        builder.Property(e => e.Type)
            .HasConversion(
                type => type.Value,
                value => ExerciseType.FromValue(value));
    }
}
