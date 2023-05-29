
using CodeGo.Api;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApi();
}

var app = builder.Build();
{
    app.MapControllers();
    app.Run();
}
