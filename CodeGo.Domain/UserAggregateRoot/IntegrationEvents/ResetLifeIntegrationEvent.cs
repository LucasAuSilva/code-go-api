
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.UserAggregateRoot.IntegrationEvents;

public record ResetLifeIntegrationEvent(
    Guid UserId) : IIntegrationEvent;
