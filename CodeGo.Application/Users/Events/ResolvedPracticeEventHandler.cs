
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.LessonTrackingAggregateRoot.Events;
using CodeGo.Domain.RankingAggregateRoot;
using MediatR;

namespace CodeGo.Application.Users.Events;

public class ResolvedPracticeEventHandler : INotificationHandler<ResolvedPracticeEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly IRankingRepository _rankingRepository;

    public ResolvedPracticeEventHandler(
        IUserRepository userRepository,
        IRankingRepository rankingRepository)
    {
        _userRepository = userRepository;
        _rankingRepository = rankingRepository;
    }

    public async Task Handle(
        ResolvedPracticeEvent notification,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindById(notification.UserId)
            ?? throw new NullReferenceException($"An user with this id {notification.UserId.Value} does't exist");
        user.ResolvePractice(
            notification.Practice.IsCorrect,
            notification.Difficulty);
        var ranking = await _rankingRepository.FindByCourseIdAndPeriod(notification.LessonTracking.CourseId, DateTime.UtcNow);
        if (ranking is null)
        {
            ranking = Ranking.CreateNew(notification.LessonTracking.CourseId);
            await _rankingRepository.AddAsync(ranking);
        }
        ranking.UpdateUserRankingProgress(
            user,
            notification.Practice.IsCorrect,
            notification.Difficulty);
        await _userRepository.Update(user);
    }
}
