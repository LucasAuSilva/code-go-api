
using CodeGo.Domain.ExerciseAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Exercises.Queries.ListExerciseByCourse;

public record ListExerciseByCourseQuery(
    string CourseId) : IRequest<ErrorOr<List<Exercise>>>;
