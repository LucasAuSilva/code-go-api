
using System.Reflection;
using CodeGo.Domain.CategoryAggregateRoot;
using CodeGo.Domain.Common.Models;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.LessonTrackingAggregateRoot;
using CodeGo.Domain.ProgressAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot;
using CodeGo.Domain.UserAggregateRoot;
using CodeGo.Infrastructure.Middleware;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CodeGo.Infrastructure.Persistance;

public class CodeGoDbContext : DbContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CodeGoDbContext(
        DbContextOptions<CodeGoDbContext> options,
        IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Question> Questions { get; set; } = null!;
    public DbSet<Exercise> Exercises { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Progress> Progresses { get; set; } = null!;
    public DbSet<LessonTracking> LessonTrackings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        if (_httpContextAccessor.HttpContext is null)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        var domainEvents = ChangeTracker.Entries<IHasDomainEvents>()
           .Select(entry => entry.Entity.PopDomainEvents())
           .SelectMany(x => x)
           .ToList();

        var result = await base.SaveChangesAsync(cancellationToken);

        Queue<IDomainEvent> domainEventsQueue = _httpContextAccessor.HttpContext!.Items.TryGetValue(EventualConsistencyMiddleware.DomainEventsKey, out var value) &&
            value is Queue<IDomainEvent> existingDomainEvents
            ? existingDomainEvents
            : new();

        domainEvents.ForEach(domainEventsQueue.Enqueue);
        _httpContextAccessor.HttpContext.Items[EventualConsistencyMiddleware.DomainEventsKey] = domainEventsQueue;

        return result;
    }
}
