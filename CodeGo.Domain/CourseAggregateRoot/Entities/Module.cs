
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.Enums;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.CourseAggregateRoot.Entities;

public sealed class Module : Entity<ModuleId>
{
    public string Name { get; private set; }
    public int TotalLessons { get; private set; }
    public int Position { get; private set; }
    public ModuleType Type { get; private set; }
    public Difficulty Difficulty { get; private set; }
    public CategoryId CategoryId { get; private set; }

    private Module(
        ModuleId id,
        string name,
        int totalLessons,
        int position,
        ModuleType type,
        Difficulty difficulty,
        CategoryId categoryId) : base(id)
    {
        Name = name;
        TotalLessons = totalLessons;
        Position = position;
        Type = type;
        Difficulty = difficulty;
        CategoryId = categoryId;
    }

    public static Module CreateNew(
        string name,
        int totalLesson,
        int position,
        ModuleType type,
        Difficulty difficulty,
        CategoryId categoryId)
    {
        return new Module(
            id: ModuleId.CreateNew(),
            name: name,
            totalLessons: totalLesson,
            position: position,
            type: type,
            difficulty: difficulty,
            categoryId: categoryId);
    }

#pragma warning disable CS8618
    private Module() {}
#pragma warning restore CS8618
}
