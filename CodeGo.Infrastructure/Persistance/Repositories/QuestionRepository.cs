
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot.Entities;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private static readonly List<Question> _questions = new();
    public List<Question> FindByCourseId(Guid courseId)
    {
        return _questions
            .Where(q => q.CourseId.Value == courseId)
            .ToList();
    }
}
