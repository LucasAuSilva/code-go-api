
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
        notification.User.ResolvePractice(
            notification.Practice.IsCorrect,
            notification.Difficulty);
        var ranking = await _rankingRepository.FindByCourseIdAndPeriod(notification.LessonTracking.CourseId, DateTime.UtcNow);
        if (ranking is null)
        {
            ranking = Ranking.CreateNew(notification.LessonTracking.CourseId);
            await _rankingRepository.AddAsync(ranking);
        }
        ranking.UpdateUserRankingProgress(
            notification.User,
            notification.Practice.IsCorrect,
            notification.Difficulty);
        await _userRepository.Update(notification.User);
    }
}
