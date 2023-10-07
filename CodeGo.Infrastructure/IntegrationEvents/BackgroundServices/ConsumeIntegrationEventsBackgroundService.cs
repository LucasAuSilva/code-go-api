
using System.Text;
using System.Text.Json;
using CodeGo.Domain.Common.Models;
using CodeGo.Infrastructure.Broker.Settings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CodeGo.Infrastructure.IntegrationEvents.BackgroundServices;

public class ConsumeIntegrationEventsBackgroundService : IHostedService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<ConsumeIntegrationEventsBackgroundService> _logger;
    private readonly CancellationTokenSource _cts;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private readonly BrokerSettings _brokerSettings;
    private readonly LifeQueueSettings _lifeQueueSettings;

    public ConsumeIntegrationEventsBackgroundService(
        ILogger<ConsumeIntegrationEventsBackgroundService> logger,
        IServiceScopeFactory serviceScopeFactory,
        IOptions<BrokerSettings> brokerSettings,
        IOptions<LifeQueueSettings> lifeQueueSettings)
    {
        _logger = logger;
        _cts = new CancellationTokenSource();
        _serviceScopeFactory = serviceScopeFactory;
        _brokerSettings = brokerSettings.Value;
        _lifeQueueSettings = lifeQueueSettings.Value;
        IConnectionFactory connectionFactory = new ConnectionFactory
        {
            HostName = _brokerSettings.Host,
            Port = _brokerSettings.Port,
            UserName = _brokerSettings.Username,
            Password = _brokerSettings.Password
        };
        _connection = connectionFactory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(
            queue: _lifeQueueSettings.QueueName,
            durable: false,
            exclusive: false,
            autoDelete: false);
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += PublishIntegrationEvent;
        _channel.BasicConsume(_lifeQueueSettings.QueueName, autoAck: false, consumer);
    }

    private async void PublishIntegrationEvent(object? sender, BasicDeliverEventArgs eventArgs)
    {
        if (_cts.IsCancellationRequested)
        {
            _logger.LogInformation("Cancellation requested, not consuming integration event.");
            return;
        }

        try
        {
            _logger.LogInformation("Received integration event. Reading message from queue.");

            using var scope = _serviceScopeFactory.CreateScope();

            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var integrationEvent = JsonSerializer.Deserialize<IIntegrationEvent>(message);
            if (integrationEvent is null)
            {
                _logger.LogInformation("Integration event came null");
                return;
            }

            _logger.LogInformation(
                "Received integration event of type: {IntegrationEventType}. Publishing event.",
                integrationEvent.GetType().Name);

            var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();
            await publisher.Publish(integrationEvent);

            _logger.LogInformation("Integration event published in Gym Management service successfully. Sending ack to message broker.");

            _channel.BasicAck(eventArgs.DeliveryTag, multiple: false);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Exception occurred while consuming integration event");
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting integration event consumer background service.");
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _cts.Cancel();
        _cts.Dispose();
        return Task.CompletedTask;
    }
}
