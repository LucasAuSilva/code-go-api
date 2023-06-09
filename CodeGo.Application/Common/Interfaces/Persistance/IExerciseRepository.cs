
using CodeGo.Domain.ExerciseAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IExerciseRepository
{
    List<Exercise> FindByCourseId(Guid courseId);
}
