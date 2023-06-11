
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private readonly CodeGoDbContext _dbContext;

    public ExerciseRepository(CodeGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Exercise>> FindByCourseId(CourseId courseId)
    {
        return await _dbContext.Exercises
            .Where(e => e.CourseId == courseId)
            .ToListAsync();
    }

    public async Task Add(Exercise exercise)
    {
        await _dbContext.AddAsync(exercise);
        await _dbContext.SaveChangesAsync();
    }
}
