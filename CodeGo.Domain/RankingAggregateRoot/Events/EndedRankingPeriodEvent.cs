
using CodeGo.Domain.Common.Models;

namespace CodeGo.Domain.RankingAggregateRoot.Events;

public record EndedRankingPeriodEvent(
    Ranking Ranking) : IDomainEvent;
