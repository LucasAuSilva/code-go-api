
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.MissionAggregateRoot.Enums;
using CodeGo.Domain.MissionAggregateRoot.ValueObjects;

namespace CodeGo.Domain.MissionAggregateRoot;

public sealed class Mission : AggregateRoot<MissionId, Guid>
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Icon { get; private set; }
    public MissionType Type { get; private set; }
    public TimeSpan Time { get; private set; }
    public Goal Goal { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdateAt { get; private set; }

    private Mission(
        MissionId id,
        string title,
        string description,
        string icon,
        MissionType type,
        TimeSpan time,
        Goal goal,
        DateTime createdAt,
        DateTime updatedAt) : base(id)
    {
        Title = title;
        Description = description;
        Icon = icon;
        Type = type;
        Time = time;
        Goal = goal;
        CreatedAt = createdAt;
        UpdateAt = updatedAt;
    }

    public static Mission CreateNew(
        string title,
        string description,
        string icon,
        MissionType type,
        TimeSpan time,
        Goal goal)
    {
        return new Mission(
            MissionId.CreateNew(),
            title,
            description,
            icon,
            type,
            time,
            goal,
            DateTime.UtcNow,
            DateTime.UtcNow);
    }


    public override MissionId IdToValueObject()
    {
        return MissionId.Create(Id.Value);
    }

#pragma warning disable CS8618
    private Mission() {}
#pragma warning restore CS8618
}
