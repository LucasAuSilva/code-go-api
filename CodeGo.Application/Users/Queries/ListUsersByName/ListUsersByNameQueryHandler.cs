
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Common.Results;
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Queries.ListUsersByName;

public class ListUsersByNameQueryHandler : IRequestHandler<ListUsersByNameQuery, ErrorOr<PagedListResult<User>>>
{
    private readonly IUserRepository _userRepository;

    public ListUsersByNameQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<PagedListResult<User>>> Handle(
        ListUsersByNameQuery query,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.ListUsersByName(
            query.Name);
        return PagedListResult<User>.Create(users, query.Page, query.PageSize);
    }
}
