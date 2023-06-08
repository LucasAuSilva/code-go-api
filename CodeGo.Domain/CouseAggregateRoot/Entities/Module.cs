
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.CouseAggregateRoot.Enums;
using CodeGo.Domain.CouseAggregateRoot.ValueObjects;

namespace CodeGo.Domain.CouseAggregateRoot.Entities;

public sealed class Module : Entity<ModuleId>
{
    public string Name { get; }
    public int TotalLessons { get; }
    public ModuleType Type { get; }
    public Difficulty Difficulty { get; }

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
        ModuleType type,
        Difficulty difficulty)
    {
        return new Module(
            ModuleId.CreateNew(),
            name,
            5,
            type,
            difficulty);
    }
}
