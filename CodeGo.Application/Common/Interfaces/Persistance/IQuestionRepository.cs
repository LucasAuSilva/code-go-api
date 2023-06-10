
using CodeGo.Domain.QuestionAggregateRoot;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IQuestionRepository
{
    List<Question> FindByCourseId(Guid courseId);
}
