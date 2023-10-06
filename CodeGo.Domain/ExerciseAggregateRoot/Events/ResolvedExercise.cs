using CodeGo.Domain.Common.Models;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.ExerciseAggregateRoot.Events;

public record ResolvedExercise(
    UserId UserId,
    Exercise Exercise,
    bool IsCorrect) : IDomainEvent;
