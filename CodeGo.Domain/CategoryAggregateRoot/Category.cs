
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Enums;
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.CategoryAggregateRoot;

public sealed class Category : AggregateRoot<CategoryId, Guid>
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Language Language { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    private Category(
        CategoryId id,
        string name,
        string description,
        Language language,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        Name = name;
        Description = description;
        Language = language;
        CreatedAt = createdAt;
        UpdatedAt = updatedAt;
    }

    public static Category CreateNew(
        string name,
        string description,
        Language language)
    {
        return new Category(
            id: CategoryId.CreateNew(),
            name: name,
            description: description,
            language: language,
            createdAt: DateTime.UtcNow,
            updatedAt: DateTime.UtcNow);
    }

    public override CategoryId IdToValueObject()
    {
        return CategoryId.Create(Id.Value);
    }

#pragma warning disable CS8618
    private Category() {}
#pragma warning restore CS8618
}
