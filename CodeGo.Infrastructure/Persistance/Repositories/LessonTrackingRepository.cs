

using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.LessonTrackingAggregateRoot;

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
}
