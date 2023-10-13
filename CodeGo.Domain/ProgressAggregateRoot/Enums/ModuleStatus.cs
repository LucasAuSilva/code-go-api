
using Ardalis.SmartEnum;

namespace CodeGo.Domain.ProgressAggregateRoot.Enums;

public sealed class ModuleStatus : SmartEnum<ModuleStatus>
{
    public static readonly ModuleStatus Completed = new(nameof(Completed), 1);
    public static readonly ModuleStatus Current = new(nameof(Current), 2);

    public ModuleStatus(string name, int value) : base(name, value)
    {}
}
