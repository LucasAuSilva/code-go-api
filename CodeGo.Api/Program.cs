
using CodeGo.Api;
using CodeGo.Application;
using CodeGo.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApi()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}

var app = builder.Build();
{
    app.UseExceptionHandler("/error");
    app.AddInfrastructureMiddleware();
    if (app.Environment.IsDocker() || app.Environment.IsProduction())
    {
        app.MigrationInitialization();
    }
    app.MapControllers();
    app.UseCors();
    app.Run();
}
