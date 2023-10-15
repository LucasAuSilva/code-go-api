
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ProgressAggregateRoot;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Progresses.Queries.GetProgressDetails;

public class GetProgressDetailsQueryHandler : IRequestHandler<GetProgressDetailsQuery, ErrorOr<Progress>>
{
    private readonly IProgressRepository _progressRepository;
    private readonly ICourseRepository _courseRepository;

    public GetProgressDetailsQueryHandler(IProgressRepository progressRepository, ICourseRepository courseRepository)
    {
        _progressRepository = progressRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<Progress>> Handle(
        GetProgressDetailsQuery query,
        CancellationToken cancellationToken)
    {
        var courseId = CourseId.Create(query.CourseId);
        var progress = await _progressRepository.FindByUserIdAndCourseId(
            UserId.Create(query.UserId),
            courseId);
        if (progress is null)
            return Errors.Progresses.NotFoundByUserIdAndCourseId;
        var course = await _courseRepository.FindById(courseId);
        if (course is null)
            return Errors.Course.CourseNotFound;
        return progress;
    }
}
