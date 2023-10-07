
using CodeGo.Domain.Common.Models;
using CodeGo.Infrastructure.IntegrationEvents.Settings;

namespace CodeGo.Infrastructure.IntegrationEvents.IntegrationEventsPublisher;

public interface IIntegrationEventsPublisher
{
    public void PublishEvent(IIntegrationEvent integrationEvent, IQueueSettings queueSettings);
}
