
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CourseAggregateRoot.Enums;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.CourseAggregateRoot.Entities;

public sealed class Module : Entity<ModuleId>
{
    public string Name { get; private set; }
    public int TotalLessons { get; private set; }
    public ModuleType Type { get; private set; }
    public Difficulty Difficulty { get; private set; }

    private Module(
        ModuleId id,
        string name,
        int totalLessons,
        ModuleType type,
        Difficulty difficulty) : base(id)
    {
        Name = name;
        TotalLessons = totalLessons;
        Type = type;
        Difficulty = difficulty;
    }

    public static Module CreateNew(
        string name,
        int totalLesson,
        ModuleType type,
        Difficulty difficulty)
    {
        return new Module(
            id: ModuleId.CreateNew(),
            name: name,
            totalLessons: totalLesson,
            type: type,
            difficulty: difficulty);
    }

#pragma warning disable CS8618
    private Module() {}
#pragma warning restore CS8618
}
