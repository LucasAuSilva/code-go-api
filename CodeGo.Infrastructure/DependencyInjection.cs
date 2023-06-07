
using System.Text;
using CodeGo.Application.Common.Interfaces.Authentication;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Infrastructure.Authentication;
using CodeGo.Infrastructure.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace CodeGo.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services
            .AddAuthentication(configuration)
            .AddPersistance();
        return services;
    }

    private static IServiceCollection AddPersistance(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILevelRepository, LevelRepository>();
        return services;
    }

    private static IServiceCollection AddAuthentication(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.Bind(JwtSettings.SectionName, jwtSettings);
        services.AddSingleton(Options.Create(jwtSettings));
        services.AddScoped<IJwtTokenGenerator, JwtTokenHandler>();
        services.AddScoped<IHashGenerator, HashHandler>();
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret))
            });
        return services;
    }
}
