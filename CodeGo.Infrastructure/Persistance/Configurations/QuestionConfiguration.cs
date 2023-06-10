
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGo.Infrastructure.Persistance.Configurations;

public class QuestionConfiguration : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        ConfigureQuestionTable(builder);
        ConfigureAlternativeTable(builder);
    }

    private static void ConfigureAlternativeTable(EntityTypeBuilder<Question> builder)
    {
        builder.OwnsMany(q => q.Alternatives, ab =>
        {
            ab.ToTable("alternatives");
            ab.WithOwner().HasForeignKey("QuestionId");
            ab.HasKey("QuestionId", "Id");
            ab.Property(a => a.Id)
                .HasColumnName("AlternativeId")
                .ValueGeneratedNever()
                .HasConversion(
                    id => id.Value,
                    value => AlternativeId.Create(value));
        });
        builder.Metadata.FindNavigation(nameof(Question.Alternatives))!
            .SetPropertyAccessMode(PropertyAccessMode.Field);
    }

    private static void ConfigureQuestionTable(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("questions");
        builder.HasKey(q => q.Id);
        builder.Property(q => q.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => QuestionId.Create(value));
        builder.Property(q => q.Title)
            .HasMaxLength(80);
        builder.OwnsOne(q => q.Difficulty);
        builder.Property(q => q.CourseId)
            .HasConversion(
                id => id.Value,
                value => CourseId.Create(value));
        builder.Property(q => q.CategoryId)
            .HasConversion(
                id => id.Value,
                value => CategoryId.Create(value));
    }
}
