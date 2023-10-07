
namespace CodeGo.Infrastructure.Broker.Settings;

public class BrokerSettings
{
    public static readonly string SectionName = "BrokerSettings";
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Host { get; set; } = null!;
    public int Port { get; set; }
}
