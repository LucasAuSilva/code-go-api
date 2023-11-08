
using CodeGo.Application.Common.Results;
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Queries.ListUsersByName;

public record ListUsersByNameQuery(
    string LoggedUserId,
    string? Name,
    int Page,
    int PageSize
) : IRequest<ErrorOr<PagedListResult<User>>>;
