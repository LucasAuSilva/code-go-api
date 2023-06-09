
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot;

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

    public async Task<Course?> FindById(Guid courseId)
    {
        return await _dbContext.Courses
            .FindAsync(courseId.ToString());
    }
}
