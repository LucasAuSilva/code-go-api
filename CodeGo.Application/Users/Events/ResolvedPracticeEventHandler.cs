
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.LessonTrackingAggregateRoot.Events;
using MediatR;

namespace CodeGo.Application.Users.Events;

public class ResolvedPracticeEventHandler : INotificationHandler<ResolvedPracticeEvent>
{
    private readonly IUserRepository _userRepository;

    public ResolvedPracticeEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(
        ResolvedPracticeEvent notification,
        CancellationToken cancellationToken)
    {
        // TODO: probably going to update the CategoryProgress on difficulty here 
        var user = await _userRepository.FindById(notification.UserId)
            ?? throw new NullReferenceException($"An user with this id {notification.UserId.Value} does't exist");
        user.ResolvePractice(
            notification.Practice.IsCorrect,
            notification.Difficulty);
        await _userRepository.Update(user);
    }
}
