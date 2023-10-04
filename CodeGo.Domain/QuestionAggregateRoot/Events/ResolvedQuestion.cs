
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.QuestionAggregateRoot.Events;

public record ResolvedQuestion(
    UserId UserId,
    Question Question,
    bool IsCorrect) : IDomainEvent;
