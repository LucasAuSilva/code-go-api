
using System.Text.Json.Serialization;
using CodeGo.Domain.RankingAggregateRoot.Events;
using CodeGo.Domain.RankingAggregateRoot.IntegrationEvents;
using CodeGo.Domain.UserAggregateRoot.IntegrationEvents;
using MediatR;

namespace CodeGo.Domain.Common.Models;

[JsonDerivedType(typeof(ResetLifeIntegrationEvent), typeDiscriminator: nameof(ResetLifeIntegrationEvent))]
[JsonDerivedType(typeof(EndedRankingPeriodIntegrationEvent), typeDiscriminator: nameof(EndedRankingPeriodEvent))]
public interface IIntegrationEvent : INotification {} 
