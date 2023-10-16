
using CodeGo.Domain.QuestionAggregateRoot;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Questions.Queries.ListQuestionsByCourse;

public record ListQuestionsByCourseQuery(
    string CourseId) : IRequest<ErrorOr<List<Question>>>;
