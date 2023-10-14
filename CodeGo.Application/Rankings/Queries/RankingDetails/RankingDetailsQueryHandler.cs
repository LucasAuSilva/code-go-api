
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.RankingAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Rankings.Queries.RankingDetails;

public class RankingDetailsQueryHandler : IRequestHandler<RankingDetailsQuery, ErrorOr<Ranking>>
{
    private readonly IRankingRepository _rankingRepository;

    public RankingDetailsQueryHandler(IRankingRepository rankingRepository)
    {
        _rankingRepository = rankingRepository;
    }

    public async Task<ErrorOr<Ranking>> Handle(
        RankingDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var courseId = CourseId.Create(query.CourseId);
        var ranking = await _rankingRepository.FindByCourseIdAndPeriod(
            courseId, DateTime.UtcNow);
        if (ranking is null)
        {
            ranking = Ranking.CreateNew(courseId);
            await _rankingRepository.AddAsync(ranking);
        }
        return ranking;
    }
}
