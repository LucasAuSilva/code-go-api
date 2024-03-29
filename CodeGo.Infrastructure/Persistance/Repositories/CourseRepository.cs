
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly CodeGoDbContext _dbContext;

    public CourseRepository(CodeGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Course course)
    {
        await _dbContext.AddAsync(course);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Course?> FindById(CourseId courseId)
    {
        return await _dbContext.Courses.FirstOrDefaultAsync(course => course.Id == courseId);
    }

    public async Task Update(Course course)
    {
        _dbContext.Update(course);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> Exists(CourseId courseId)
    {
        return await _dbContext.Courses.AnyAsync(course => course.Id == courseId);
    }

    public async Task<List<Course>> ListAsync()
    {
        return await _dbContext.Courses.ToListAsync();
    }
}
