
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;

namespace CodeGo.Domain.LessonTrackingAggregateRoot.Events;

public record CancelPreviousLessonsEvent(
    LessonTrackingId ActiveLessonTrackingId) : IDomainEvent;
