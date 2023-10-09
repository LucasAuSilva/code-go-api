
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot.ValueObjects;

namespace CodeGo.Domain.ProgressAggregateRoot.Entities;

public sealed class CategoryTracking : Entity<CategoryTrackingId>
{
    public CategoryId CategoryId { get; private set; }
    public Difficulty DifficultyLevel { get; private set; }

    private CategoryTracking(
        CategoryTrackingId id,
        CategoryId categoryId,
        Difficulty difficulty) : base(id)
    {
        CategoryId = categoryId;
        DifficultyLevel = difficulty;
    }

    public static CategoryTracking CreateNew(
        CategoryId categoryId,
        Difficulty difficulty)
    {
        return new CategoryTracking(
            CategoryTrackingId.CreateNew(),
            categoryId,
            difficulty);
    }
}
