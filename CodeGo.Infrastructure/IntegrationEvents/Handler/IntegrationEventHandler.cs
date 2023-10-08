
using CodeGo.Domain.UserAggregateRoot.Events;
using CodeGo.Domain.UserAggregateRoot.IntegrationEvents;
using CodeGo.Infrastructure.Broker.Settings;
using CodeGo.Infrastructure.IntegrationEvents.IntegrationEventsPublisher;
using MediatR;
using Microsoft.Extensions.Options;

namespace CodeGo.Infrastructure.IntegrationEvents.Handler;

public class IntegrationEventHandler :
    INotificationHandler<ResetLifeEvent>
{
    private readonly IIntegrationEventsPublisher _integrationEventPublisher;
    private readonly LifeQueueSettings _lifeQueueSettings;

    public IntegrationEventHandler(
        IIntegrationEventsPublisher integrationEventPublisher,
        IOptions<LifeQueueSettings> lifeQueueSettings)
    {
        _integrationEventPublisher = integrationEventPublisher;
        _lifeQueueSettings = lifeQueueSettings.Value;
    }

    public Task Handle(ResetLifeEvent notification, CancellationToken cancellationToken)
    {
        var integrationEvent = new ResetLifeIntegrationEvent(notification.UserId.Value);
        _integrationEventPublisher.PublishEvent(integrationEvent, _lifeQueueSettings);
        return Task.CompletedTask;
    }
}
