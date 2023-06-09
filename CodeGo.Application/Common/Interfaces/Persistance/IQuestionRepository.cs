
using CodeGo.Domain.QuestionAggregateRoot.Entities;

namespace CodeGo.Application.Common.Interfaces.Persistance;

public interface IQuestionRepository
{
    List<Question> FindByCourseId(Guid courseId);
}
