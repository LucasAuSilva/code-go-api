
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.QuestionAggregateRoot.Events;
using MediatR;

namespace CodeGo.Application.Users.Events;

public class ResolvedQuestionEventHandler : INotificationHandler<ResolvedQuestion>
{
    private readonly IUserRepository _userRepository;

    public ResolvedQuestionEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(
        ResolvedQuestion notification,
        CancellationToken cancellationToken)
    {
        // TODO: probably going to update the CategoryProgress on difficulty here 
        var user = await _userRepository.FindById(notification.UserId)
            ?? throw new NullReferenceException($"An user with this id {notification.UserId.Value} does't exist");
        user.ResolvePractice(
            notification.IsCorrect,
            notification.Question.Difficulty);
        await _userRepository.Update(user);
    }
}
