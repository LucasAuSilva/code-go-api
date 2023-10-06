
namespace CodeGo.Contracts.Users;

public record ListUsersByNameRequest(
    string? Name,
    int Page,
    int PageSize);
