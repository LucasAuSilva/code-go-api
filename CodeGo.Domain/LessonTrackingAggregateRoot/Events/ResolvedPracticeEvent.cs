
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.LessonTrackingAggregateRoot.Entities;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.LessonTrackingAggregateRoot.Events;

public record ResolvedPracticeEvent(
    Practice Practice,
    Difficulty Difficulty,
    UserId UserId
) : IDomainEvent;
