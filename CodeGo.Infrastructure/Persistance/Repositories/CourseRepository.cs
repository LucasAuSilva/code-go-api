
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly CodeGoDbContext _dbContext;

    public CourseRepository(CodeGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Course level)
    {
        _dbContext.Add(level);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Course?> FindById(CourseId courseId)
    {
        return await _dbContext.Courses
            .FindAsync(courseId);
    }

    public async Task Update(Course course)
    {
        _dbContext.Update(course);
        await _dbContext.SaveChangesAsync();
    }
}
