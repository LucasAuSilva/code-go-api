
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IQuestionRepository
{
    Task<Question?> FindById(QuestionId questionId);
    Task<List<Question>> FindByCourseId(CourseId courseId);
    Task Add(Question question);
    Task UpdateAsync(Question question);
    Task DeleteAsync(Question question);
    Task SaveChangesAsync();
}
