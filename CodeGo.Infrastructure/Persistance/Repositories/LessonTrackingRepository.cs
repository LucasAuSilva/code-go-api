

using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.LessonTrackingAggregateRoot;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using Microsoft.EntityFrameworkCore;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class LessonTrackingRepository : ILessonTrackingRepository
{
    private readonly CodeGoDbContext _dbContext;

    public LessonTrackingRepository(CodeGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(LessonTracking lessonTracking)
    {
        await _dbContext.AddAsync(lessonTracking);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<LessonTracking>> FindByStatus(LessonStatus status)
    {
        return await _dbContext.LessonTrackings
            .Where(lesson => lesson.Status == LessonStatus.InProgress)
            .ToListAsync();
    }

    public async Task UpdateManyAsync(List<LessonTracking> lessons)
    {
        _dbContext.UpdateRange(lessons);
        await _dbContext.SaveChangesAsync(); 
    }
}
