
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Courses.Common;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Courses.Queries.ListPractices;

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
        // TODO: Check if user is registered in the course
        // TODO: Check if user can start this module
        var course = await _courseRepository.FindById(CourseId.Create(query.CourseId));
        if (course is null)
            return Errors.Course.CourseNotFound;
        var moduleId = ModuleId.Create(query.ModuleId);
        var courseId = CourseId.Create(course.Id.Value);
        if (!course.HasModule(moduleId))
            return Errors.Course.ModuleNotFound;

        var courseQuestions = await _questionRepository.FindByCourseId(courseId);
        var courseExercises = await _exerciseRepository.FindByCourseId(courseId);

        var moduleQuestions = course.SelectModuleQuestions(courseQuestions, moduleId);
        var moduleExercises = course.SelectModuleExercises(courseExercises, moduleId);

        return new PracticesResult(
            moduleQuestions,
            moduleExercises);
    }
}
