
using CodeGo.Domain.CourseAggregateRoot;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeGo.Infrastructure.Persistance;

public class CodeGoDbContext : DbContext
{
    public CodeGoDbContext(DbContextOptions<CodeGoDbContext> options)
        : base(options)
    {}

    public DbSet<Course> Courses { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ApplyConfigurationsFromAssembly(typeof(CodeGoDbContext).Assembly);

        // apply config to all entities
        // modelBuilder.Model.GetEntityTypes()
        //     .SelectMany(e => e.GetProperties())
        //     .Where(p => p.IsPrimaryKey())
        //     .ToList()
        //     .ForEach(p => p.ValueGenerated = ValueGenerated.Never);

        base.OnModelCreating(modelBuilder);
    }
}
