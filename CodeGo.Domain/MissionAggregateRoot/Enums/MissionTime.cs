
using Ardalis.SmartEnum;

namespace CodeGo.Domain.MissionAggregateRoot.Enums;

public class MissionTime : SmartEnum<MissionTime>
{
    public static readonly MissionTime Day = new(nameof(Day), 1);
    public static readonly MissionTime Week = new(nameof(Week), 2);
    public static readonly MissionTime HalfMonth = new(nameof(HalfMonth), 2);
    public static readonly MissionTime Month = new(nameof(Month), 2);

    public MissionTime(string name, int value) : base(name, value)
    {}
}
