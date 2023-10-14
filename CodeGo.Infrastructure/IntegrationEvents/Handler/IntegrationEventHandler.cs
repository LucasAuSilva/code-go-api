
using CodeGo.Domain.RankingAggregateRoot.Events;
using CodeGo.Domain.RankingAggregateRoot.IntegrationEvents;
using CodeGo.Domain.RankingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.Events;
using CodeGo.Domain.UserAggregateRoot.IntegrationEvents;
using CodeGo.Infrastructure.Broker.Settings;
using CodeGo.Infrastructure.IntegrationEvents.IntegrationEventsPublisher;
using CodeGo.Infrastructure.IntegrationEvents.Settings;
using MediatR;
using Microsoft.Extensions.Options;

namespace CodeGo.Infrastructure.IntegrationEvents.Handler;

public class IntegrationEventHandler :
    INotificationHandler<ResetLifeEvent>,
    INotificationHandler<EndedRankingPeriodEvent>
{
    private readonly IIntegrationEventsPublisher _integrationEventPublisher;
    private readonly LifeQueueSettings _lifeQueueSettings;
    private readonly RankingQueueSettings _rankingQueueSettings;

    public IntegrationEventHandler(
        IIntegrationEventsPublisher integrationEventPublisher,
        IOptions<LifeQueueSettings> lifeQueueSettings,
        IOptions<RankingQueueSettings> rankingQueueSettings)
    {
        _integrationEventPublisher = integrationEventPublisher;
        _lifeQueueSettings = lifeQueueSettings.Value;
        _rankingQueueSettings = rankingQueueSettings.Value;
    }

    public Task Handle(ResetLifeEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ResetLifeIntegrationEvent(notification.UserId.Value);
        _integrationEventPublisher.PublishEvent(integrationEvent, _lifeQueueSettings);
        return Task.CompletedTask;
    }

    public Task Handle(EndedRankingPeriodEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new EndedRankingPeriodIntegrationEvent(
            RankingId.Create(notification.Ranking.Id.Value));
        _rankingQueueSettings.DelayInMinutes = notification.Ranking.Period.InMinutes();
        _integrationEventPublisher.PublishEvent(integrationEvent, _rankingQueueSettings);
        return Task.CompletedTask;
    }
}
