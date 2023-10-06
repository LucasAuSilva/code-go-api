using CodeGo.Domain.UserAggregateRoot;

namespace CodeGo.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token);
