
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IQuestionRepository
{
    Task<List<Question>> FindByCourseId(CourseId courseId);
    Task Add(Question question);
}
