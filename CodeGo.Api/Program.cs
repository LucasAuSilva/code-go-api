
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
    Console.WriteLine(app.Environment.IsDocker());
    if (app.Environment.IsDocker())
    {
        Console.WriteLine("Hello Docker");
        app.MigrationInitialization();
    }
    app.UseExceptionHandler("/error");
    app.MapControllers();
    app.Run();
}
