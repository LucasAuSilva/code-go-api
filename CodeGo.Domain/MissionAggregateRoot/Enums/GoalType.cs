
using Ardalis.SmartEnum;

namespace CodeGo.Domain.MissionAggregateRoot.Enums;

public class GoalType : SmartEnum<GoalType>
{
    public static readonly GoalType Lesson = new(nameof(Lesson), 1);
    public static readonly GoalType Module = new(nameof(Module), 1);
    public static readonly GoalType Points = new(nameof(Points), 1);
    public static readonly GoalType LessonScore = new(nameof(LessonScore), 1);
    public static readonly GoalType Mission = new(nameof(Mission), 1);
    public GoalType(string name, int value) : base(name, value)
    {
    }
}
