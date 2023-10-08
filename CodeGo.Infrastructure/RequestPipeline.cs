
using CodeGo.Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace CodeGo.Infrastructure;

public static class RequestPipeline
{
    public static IApplicationBuilder AddInfrastructureMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<EventualConsistencyMiddleware>();
        return app;
    }
}
