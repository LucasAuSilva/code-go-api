
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;
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

    public async Task<Exercise?> FindById(ExerciseId exerciseId)
    {
        return await _dbContext.Exercises
            .FirstOrDefaultAsync(exercise => exercise.Id == exerciseId);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Exercise exercise)
    {
        _dbContext.Update(exercise);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Exercise exercise)
    {
        _dbContext.Exercises.Remove(exercise);
        await _dbContext.SaveChangesAsync();
    }
}
