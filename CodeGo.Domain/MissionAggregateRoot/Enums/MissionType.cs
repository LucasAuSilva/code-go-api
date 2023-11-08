
using Ardalis.SmartEnum;

namespace CodeGo.Domain.MissionAggregateRoot.Enums;

public class MissionType : SmartEnum<MissionType>
{
    public static readonly MissionType Alone = new(nameof(Alone), 1);
    public static readonly MissionType Duo = new(nameof(Duo), 2);

    public MissionType(string name, int value) : base(name, value)
    {}
}
