
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.RankingAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IRankingRepository
{
    Task<Ranking?> FindByCourseIdAndPeriod(CourseId courseId, DateTime today);
    Task AddAsync(Ranking ranking);
    Task UpdateAsync(Ranking ranking);
}
