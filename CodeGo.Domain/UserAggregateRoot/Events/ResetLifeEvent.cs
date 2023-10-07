
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.UserAggregateRoot.Events;

public record ResetLifeEvent(UserId UserId) : IDomainEvent;
