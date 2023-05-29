
using CodeGo.Api.Common.Errors;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CodeGo.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, CodeGoProblemDetailsFactory>();
        return services;
    }
}
