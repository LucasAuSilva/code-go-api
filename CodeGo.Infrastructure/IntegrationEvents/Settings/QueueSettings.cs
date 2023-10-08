
namespace CodeGo.Infrastructure.IntegrationEvents.Settings;

public interface IQueueSettings
{
    public string QueueName { get; set; }
    public int DelayInMinutes { get; }
}
