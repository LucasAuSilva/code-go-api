
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Infrastructure.Persistance.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace CodeGo.Infrastructure.Persistance;

public class CodeGoDbContext : DbContext
{
    private readonly PublishDomainEventsInterceptor _publishDomainEventsInterceptor;

    public CodeGoDbContext(
        DbContextOptions<CodeGoDbContext> options,
        PublishDomainEventsInterceptor publishDomainEventsInterceptor)
        : base(options)
    {
        _publishDomainEventsInterceptor = publishDomainEventsInterceptor;
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Exercise> Exercises { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Ignore<List<IDomainEvent>>()
            .ApplyConfigurationsFromAssembly(typeof(CodeGoDbContext).Assembly);

        // apply config to all entities
        // modelBuilder.Model.GetEntityTypes()
        //     .SelectMany(e => e.GetProperties())
        //     .Where(p => p.IsPrimaryKey())
        //     .ToList()
        //     .ForEach(p => p.ValueGenerated = ValueGenerated.Never);

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_publishDomainEventsInterceptor);
        base.OnConfiguring(optionsBuilder);
    }
}
