
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.RankingAggregateRoot;
using CodeGo.Domain.RankingAggregateRoot.ValueObjects;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IRankingRepository
{
    Task <Ranking?> FindById(RankingId rankingId);
    Task<Ranking?> FindByCourseIdAndPeriod(CourseId courseId, DateTime today);
    Task AddAsync(Ranking ranking);
    Task UpdateAsync(Ranking ranking);
}
