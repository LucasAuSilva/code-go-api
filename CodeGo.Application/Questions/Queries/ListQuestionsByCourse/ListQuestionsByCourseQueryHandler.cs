
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Questions.Queries.ListQuestionsByCourse;

public class ListQuestionsByCourseQueryHandler : IRequestHandler<ListQuestionsByCourseQuery, ErrorOr<List<Question>>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IQuestionRepository _questionRepository;

    public ListQuestionsByCourseQueryHandler(
        IQuestionRepository questionRepository,
        ICourseRepository courseRepository)
    {
        _questionRepository = questionRepository;
        _courseRepository = courseRepository;
    }

    public async Task<ErrorOr<List<Question>>> Handle(
        ListQuestionsByCourseQuery query,
        CancellationToken cancellationToken)
    {
        var courseId = CourseId.Create(query.CourseId);
        var course = await _courseRepository.FindById(courseId);
        if (course is null)
            return Errors.Course.CourseNotFound;
        var questions = await _questionRepository.FindByCourseId(courseId);
        return questions;
    }
}
