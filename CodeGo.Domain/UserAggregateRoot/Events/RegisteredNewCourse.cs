
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.UserAggregateRoot.Events;

public record RegisteredNewCourseEvent(
    UserId UserId,
    Course Course) : IDomainEvent;
