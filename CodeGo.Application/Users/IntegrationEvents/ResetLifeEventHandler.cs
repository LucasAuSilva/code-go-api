
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.UserAggregateRoot.IntegrationEvents;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using MediatR;

namespace CodeGo.Application.Users.IntegrationEvents;
public class ResetLifeEventHandler : INotificationHandler<ResetLifeIntegrationEvent>
{
    private readonly IUserRepository _userRepository;

    public ResetLifeEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(ResetLifeIntegrationEvent notification, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindById(UserId.Create(notification.UserId));
        if (user is null)
            return;
        user.ResetLives();
        await _userRepository.Update(user);
    }
}
