
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.RankingAggregateRoot.Events;
using MediatR;

namespace CodeGo.Application.Users.Events;

public class FinishedRankingAsFinalistEventHandler : INotificationHandler<FinishedRankingAsFinalist>
{
    private readonly IUserRepository _userRepository;

    public FinishedRankingAsFinalistEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(
        FinishedRankingAsFinalist notification,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindById(notification.UserId);
        if (user is null)
            return;
        user.FinishedRankingAsFinalist(notification.Position);
        await _userRepository.Update(user);
    }
}
