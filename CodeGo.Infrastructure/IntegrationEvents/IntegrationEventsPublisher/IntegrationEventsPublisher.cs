
using System.Text;
using System.Text.Json;
using CodeGo.Domain.Common.Models;
using CodeGo.Infrastructure.Broker.Settings;
using CodeGo.Infrastructure.IntegrationEvents.Settings;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace CodeGo.Infrastructure.IntegrationEvents.IntegrationEventsPublisher;

public class IntegrationEventsPublisher : IIntegrationEventsPublisher
{
    private readonly BrokerSettings _brokerSettings;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public IntegrationEventsPublisher(
        IOptions<BrokerSettings> brokerSettings)
    {
        _brokerSettings = brokerSettings.Value;
        var connectionFactory = new ConnectionFactory
        {
            HostName = _brokerSettings.Host,
            Port = _brokerSettings.Port,
            UserName = _brokerSettings.Username,
            Password = _brokerSettings.Password
        };
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
    }

    public void PublishEvent(IIntegrationEvent integrationEvent, IQueueSettings queueSettings)
    {
        var props = _channel.CreateBasicProperties();
        string serializedIntegrationEvent = JsonSerializer.Serialize(integrationEvent);
        byte[] body = Encoding.UTF8.GetBytes(serializedIntegrationEvent);

        if (queueSettings.DelayInMinutes > 0)
        {
            DeclareDelayedQueue(body, props, queueSettings);
            return;
        }
        _channel.QueueDeclare(
            queue: queueSettings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false);
        _channel.BasicPublish(
            exchange: string.Empty,
            routingKey: queueSettings.QueueName,
            basicProperties: props,
            body: body);
    }

    private void DeclareDelayedQueue(
        byte[] body,
        IBasicProperties props,
        IQueueSettings queueSettings)
    {
        var args = new Dictionary<string,object>
        {
            { "x-dead-letter-exchange", "" },
            { "x-dead-letter-routing-key", _brokerSettings.InQueue }
        };
        _channel.QueueDeclare(
            queue: $"{queueSettings.QueueName}",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: args);
        var delayInMilliseconds = (int)TimeSpan.FromMinutes(queueSettings.DelayInMinutes).TotalMilliseconds;
        props.Expiration = delayInMilliseconds.ToString();
        _channel.BasicPublish(
            exchange: string.Empty,
            routingKey: $"{queueSettings.QueueName}",
            basicProperties: props,
            body: body);
    }
}
