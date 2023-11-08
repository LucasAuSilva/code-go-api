
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.MissionAggregateRoot.Enums;

namespace CodeGo.Domain.MissionAggregateRoot.ValueObjects;

public class Goal : ValueObject
{
    public GoalType Type { get; private set; }
    public int Total { get; private set; }
    public Goal? SubGoal { get; private set; }

    private Goal(GoalType type, int total, Goal? subGoal)
    {
        Type = type;
        Total = total;
        SubGoal = subGoal;
    }

    public override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Type;
        yield return Total;
        yield return SubGoal;
    }

#pragma warning disable CS8618
    private Goal() {}
#pragma warning restore CS8618
}
