
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.LessonTrackingAggregateRoot.Events;

public record FinishedLessonEvent(LessonTracking LessonTracking) : IDomainEvent;
