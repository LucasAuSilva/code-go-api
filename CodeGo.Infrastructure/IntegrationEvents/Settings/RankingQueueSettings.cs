
namespace CodeGo.Infrastructure.IntegrationEvents.Settings;

public class RankingQueueSettings : IQueueSettings
{
    public static readonly string SectionName = "RankingQueueSettings";
    public string QueueName { get; set; } = null!; 
    public int DelayInMinutes { get; set; }
}
