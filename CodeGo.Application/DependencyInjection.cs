using System.Reflection;
using CodeGo.Application.Common.Behaviors;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CodeGo.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly))
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly())
            .AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));
        return services;
    }
}
