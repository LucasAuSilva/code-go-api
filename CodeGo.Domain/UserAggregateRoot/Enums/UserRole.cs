
using Ardalis.SmartEnum;

namespace CodeGo.Domain.UserAggregateRoot.Enums;

public class UserRole : SmartEnum<UserRole>
{
    public static readonly UserRole User = new(nameof(User), 1);
    public static readonly UserRole Admin = new(nameof(Admin), 2);

    public UserRole(string name, int value) : base(name, value)
    {}
}
