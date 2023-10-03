
using CodeGo.Application.Common.Results;
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Queries.ListUsersByEmail;

public record ListUsersByEmailQuery(
    string? Email,
    int Page,
    int PageSize
) : IRequest<ErrorOr<PagedListResult<User>>>;
