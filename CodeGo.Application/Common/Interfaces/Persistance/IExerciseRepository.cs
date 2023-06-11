
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.ExerciseAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IExerciseRepository
{
    Task<List<Exercise>> FindByCourseId(CourseId courseId);
    Task Add(Exercise exercise);
}
