
using CodeGo.Domain.UserAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user);
}
