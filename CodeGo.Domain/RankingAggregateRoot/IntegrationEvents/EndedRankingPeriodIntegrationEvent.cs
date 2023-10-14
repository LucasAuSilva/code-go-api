

using CodeGo.Domain.Common.Models;
using CodeGo.Domain.RankingAggregateRoot.ValueObjects;

namespace CodeGo.Domain.RankingAggregateRoot.IntegrationEvents;

public record EndedRankingPeriodIntegrationEvent(
    RankingId RankingId) : IIntegrationEvent;
