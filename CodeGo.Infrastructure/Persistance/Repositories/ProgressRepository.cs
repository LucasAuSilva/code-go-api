

using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.ProgressAggregateRoot;

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
}
