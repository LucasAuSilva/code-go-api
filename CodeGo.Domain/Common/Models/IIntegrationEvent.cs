
using System.Text.Json.Serialization;
using CodeGo.Domain.UserAggregateRoot.IntegrationEvents;
using MediatR;

namespace CodeGo.Domain.Common.Models;

[JsonDerivedType(typeof(ResetLifeIntegrationEvent), typeDiscriminator: nameof(ResetLifeIntegrationEvent))]
public interface IIntegrationEvent : INotification {} 
