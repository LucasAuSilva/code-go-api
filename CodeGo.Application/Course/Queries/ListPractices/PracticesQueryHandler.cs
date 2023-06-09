
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Course.Common;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Course.Queries.ListPractices;

public class PracticesQueryHandler : IRequestHandler<PracticesQuery, ErrorOr<PracticesResult>>
{
    private readonly ICourseRepository _courseRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public PracticesQueryHandler(
        ICourseRepository courseRepository,
        IQuestionRepository questionRepository,
        IExerciseRepository exerciseRepository)
    {
        _courseRepository = courseRepository;
        _questionRepository = questionRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<ErrorOr<PracticesResult>> Handle(PracticesQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var course = _courseRepository.FindById(Guid.Parse(query.CourseId));
        if (course is null)
            return Errors.Course.CourseNotFound;
        var moduleId = ModuleId.Create(query.ModuleId);
        if (!course.HasModule(moduleId))
            return Errors.Course.ModuleNotFound;

        var courseQuestions = _questionRepository.FindByCourseId(course.Id.Value);
        var courseExercises = _exerciseRepository.FindByCourseId(course.Id.Value);

        var moduleQuestions = course.SelectModuleQuestions(courseQuestions, moduleId);
        var moduleExercises = course.SelectModuleExercises(courseExercises, moduleId);

        return new PracticesResult(
            moduleQuestions,
            moduleExercises);
    }
}
