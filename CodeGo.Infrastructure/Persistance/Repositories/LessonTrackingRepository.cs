

using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.LessonTrackingAggregateRoot;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
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

    public async Task<LessonTracking?> FindByIdAndUserId(LessonTrackingId id, UserId userId)
    {
        return await _dbContext.LessonTrackings
            .FirstOrDefaultAsync(lesson => lesson.Id == id && lesson.UserId == userId);
    }

    public async Task<List<LessonTracking>> FindByStatus(LessonStatus status)
    {
        return await _dbContext.LessonTrackings
            .Where(lesson => lesson.Status == LessonStatus.InProgress)
            .ToListAsync();
    }

    public async Task UpdateAsync(LessonTracking lessonTracking)
    {
        _dbContext.Update(lessonTracking);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateManyAsync(List<LessonTracking> lessons)
    {
        _dbContext.UpdateRange(lessons);
        await _dbContext.SaveChangesAsync(); 
    }
}
