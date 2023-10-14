
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.RankingAggregateRoot;
using CodeGo.Domain.RankingAggregateRoot.IntegrationEvents;
using CodeGo.Domain.RankingAggregateRoot.ValueObjects;
using MediatR;

namespace CodeGo.Application.Rankings.IntegrationEvents;

public class EndedRankingPeriodIntegrationEventHandler : INotificationHandler<EndedRankingPeriodIntegrationEvent>
{
    private readonly IRankingRepository _rankingRepository;

    public EndedRankingPeriodIntegrationEventHandler(IRankingRepository rankingRepository)
    {
        _rankingRepository = rankingRepository;
    }

    public async Task Handle(
        EndedRankingPeriodIntegrationEvent notification,
        CancellationToken cancellationToken)
    {
        var ranking = await _rankingRepository.FindById(notification.RankingId);
        if (ranking is null)
            return;
        ranking.EndRanking();
        var newRanking = Ranking.CreateNew(ranking.CourseId);
        await _rankingRepository.AddAsync(newRanking);
    }
}
