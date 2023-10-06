
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.ExerciseAggregateRoot.Events;
using MediatR;

namespace CodeGo.Application.Users.Events;

public class ResolvedExerciseEventHandler : INotificationHandler<ResolvedExercise>
{
    private readonly IUserRepository _userRepository;

    public ResolvedExerciseEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(
        ResolvedExercise notification,
        CancellationToken cancellationToken)
    {
        // TODO: probably going to update the CategoryProgress on difficulty here 
        var user = await _userRepository.FindById(notification.UserId)
            ?? throw new NullReferenceException($"An user with this id {notification.UserId.Value} does't exist");
        user.ResolvePractice(
            notification.IsCorrect,
            notification.Exercise.Difficulty);
        await _userRepository.Update(user);
    }
}
