

using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.RankingAggregateRoot;
using Microsoft.EntityFrameworkCore;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class RankingRepository : IRankingRepository
{
    private readonly CodeGoDbContext _dbContext;

    public RankingRepository(CodeGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Ranking ranking)
    {
        await _dbContext.AddAsync(ranking);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Ranking?> FindByCourseIdAndPeriod(CourseId courseId, DateTime today)
    {
        return await _dbContext.Rankings
            .FirstOrDefaultAsync(
                ranking => ranking.CourseId == courseId &&
                ranking.Period.InitialDateTime < today &&
                ranking.Period.EndDateTime > today);
    }

    public async Task UpdateAsync(Ranking ranking)
    {
        _dbContext.Update(ranking);
        await _dbContext.SaveChangesAsync();
    }
}
