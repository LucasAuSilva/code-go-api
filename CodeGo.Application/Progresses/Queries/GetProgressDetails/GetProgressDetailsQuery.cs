
using CodeGo.Domain.ProgressAggregateRoot;
using CodeGo.Domain.RankingAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Progresses.Queries.GetProgressDetails;

public record GetProgressDetailsQuery(
    string UserId,
    string CourseId) : IRequest<ErrorOr<Progress>>;
