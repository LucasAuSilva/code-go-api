
using System.Reflection;
using CodeGo.Api.Common.Errors;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CodeGo.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, CodeGoProblemDetailsFactory>();
        services.AddScoped<IMapper, ServiceMapper>();
        services.AddCors(options => {
            options.AddPolicy("_MyCorsPolicies", policy => {
                policy.AllowAnyOrigin();
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
            });
        });
        return services;
    }
}
