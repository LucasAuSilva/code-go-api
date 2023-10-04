
using System.Text;
using CodeGo.Application.Common.Interfaces.Authentication;
using CodeGo.Application.Common.Interfaces.Http;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Infrastructure.Authentication;
using CodeGo.Infrastructure.Http.Judge0Api;
using CodeGo.Infrastructure.Persistance;
using CodeGo.Infrastructure.Persistance.Interceptors;
using CodeGo.Infrastructure.Persistance.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
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
            .AddPersistance(configuration)
            .AddHttp(configuration);
        return services;
    }

    private static IServiceCollection AddHttp(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        var judge0Settings = new Judge0Settings();
        configuration.Bind(Judge0Settings.SectionName, judge0Settings);
        services.AddSingleton(Options.Create(judge0Settings));
        services.AddSingleton<ICompilerApi, CompilerApi>();
        return services;
    }

    private static IServiceCollection AddPersistance(
        this IServiceCollection services,
        ConfigurationManager configuration)
    {
        services.AddDbContext<CodeGoDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("CodeGoDatabase")));
        services.AddScoped<PublishDomainEventsInterceptor>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IExerciseRepository, ExerciseRepository>();
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
        services.AddScoped<IHashComparer, HashHandler>();
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

    public static void MigrationInitialization(this IApplicationBuilder app)
    {
        using var serviceScope = app.ApplicationServices.CreateScope();
        serviceScope.ServiceProvider.GetService<CodeGoDbContext>()?.Database.Migrate();
    }
}
