
using CodeGo.Infrastructure.IntegrationEvents.Settings;

namespace CodeGo.Infrastructure.Broker.Settings;

public class LifeQueueSettings : IQueueSettings
{
    public static readonly string SectionName = "LifeQueueSettings";
    public string QueueName { get; set; } = null!; 
    public int DelayInMinutes { get; set; }
}
