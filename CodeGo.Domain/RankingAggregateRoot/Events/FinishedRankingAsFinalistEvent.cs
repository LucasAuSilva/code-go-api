
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;

namespace CodeGo.Domain.RankingAggregateRoot.Events;

public record FinishedRankingAsFinalist(
    UserId UserId,
    int Position) : IDomainEvent;
