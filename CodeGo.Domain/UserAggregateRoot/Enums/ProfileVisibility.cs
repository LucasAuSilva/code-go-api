
using Ardalis.SmartEnum;

namespace CodeGo.Domain.UserAggregateRoot.Enums;

public class ProfileVisibility : SmartEnum<ProfileVisibility>
{
    public static readonly ProfileVisibility Public = new(nameof(Public), 1);
    public static readonly ProfileVisibility Private = new(nameof(Private), 2);

    public ProfileVisibility(string name, int value) : base(name, value)
    {}
}
