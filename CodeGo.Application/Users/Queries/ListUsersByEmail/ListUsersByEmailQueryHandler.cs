
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Common.Results;
using CodeGo.Domain.UserAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Users.Queries.ListUsersByEmail;

public class ListUsersByEmailQueryHandler : IRequestHandler<ListUsersByEmailQuery, ErrorOr<PagedListResult<User>>>
{
    private readonly IUserRepository _userRepository;

    public ListUsersByEmailQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<PagedListResult<User>>> Handle(
        ListUsersByEmailQuery query,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository.ListUsersByEmail(
            query.Email);
        return PagedListResult<User>.Create(users, query.Page, query.PageSize);
    }
}
