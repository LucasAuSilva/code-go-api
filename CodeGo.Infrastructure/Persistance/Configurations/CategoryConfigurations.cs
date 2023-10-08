
using CodeGo.Domain.CategoryAggregateRoot;
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CodeGo.Infrastructure.Persistance.Configurations;

public class CategoryConfigurations : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        ConfigureCategoryTable(builder);
    }

    private static void ConfigureCategoryTable(EntityTypeBuilder<Category> builder) 
    {
        builder.ToTable("categories");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => CategoryId.Create(value));
        builder.Property(c => c.Name)
            .HasMaxLength(100);
        builder.Property(c => c.Description)
            .HasMaxLength(350);
        builder.Property(c => c.Language)
            .HasConversion(
                language => language.Value,
                value => Language.FromValue(value));
    }
}
