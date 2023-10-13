

using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class ProgressRepository : IProgressRepository
{
    private readonly CodeGoDbContext _dbContext;

    public ProgressRepository(CodeGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Progress progress)
    {
        await _dbContext.Progresses.AddAsync(progress);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Progress?> FindByUserIdAndCourseId(UserId userId, CourseId courseId)
    {
        return await _dbContext.Progresses
            .FirstOrDefaultAsync(progress => progress.UserId == userId && progress.CourseId == courseId);
    }

    public async Task UpdateAsync(Progress progress)
    {
        _dbContext.Update(progress);
        await _dbContext.SaveChangesAsync();
    }
}
