
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.ExerciseAggregateRoot;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class ExerciseRepository : IExerciseRepository
{
    private static readonly List<Exercise> _exercise = new();
    public List<Exercise> FindByCourseId(Guid courseId)
    {
        return _exercise
            .Where(q => q.CourseId.Value == courseId)
            .ToList();
    }
}
