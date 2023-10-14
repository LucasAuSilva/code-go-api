

using CodeGo.Domain.RankingAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Rankings.Queries;

public record RankingDetailsQuery(
    string CourseId) : IRequest<ErrorOr<Ranking>>;
