
namespace CodeGo.Contracts.Users;

public record ListUsersByEmailRequest(
    string? Email,
    int Page,
    int PageSize);
