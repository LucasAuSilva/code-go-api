
namespace CodeGo.Infrastructure.Authentication;

public class JwtSettings
{
    public static readonly string SectionName = "JwtSettings";
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public string Secret { get; set; } = null!;
    public int ExpirationInMinutes { get; set; }
}
