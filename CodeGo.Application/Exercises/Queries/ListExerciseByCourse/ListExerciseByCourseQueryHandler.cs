
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Exercises.Queries.ListExerciseByCourse;

public class ListExerciseByCourseQueryHandler : IRequestHandler<ListExerciseByCourseQuery, ErrorOr<List<Exercise>>>
{
    private readonly IExerciseRepository _exerciseRepository;

    public ListExerciseByCourseQueryHandler(IExerciseRepository exerciseRepository)
    {
        _exerciseRepository = exerciseRepository;
    }

    public async Task<ErrorOr<List<Exercise>>> Handle(
        ListExerciseByCourseQuery query,
        CancellationToken cancellationToken)
    {
        var exercises = await _exerciseRepository.FindByCourseId(
            CourseId.Create(query.CourseId));
        return exercises;
    }
}
