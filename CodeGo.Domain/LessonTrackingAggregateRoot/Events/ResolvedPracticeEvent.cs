
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.Common.ValueObjects;
using CodeGo.Domain.LessonTrackingAggregateRoot.Entities;
using CodeGo.Domain.UserAggregateRoot;

namespace CodeGo.Domain.LessonTrackingAggregateRoot.Events;

public record ResolvedPracticeEvent(
    LessonTracking LessonTracking,
    Practice Practice,
    Difficulty Difficulty,
    User User
) : IDomainEvent;
