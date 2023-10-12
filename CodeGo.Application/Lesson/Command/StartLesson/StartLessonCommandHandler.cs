
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Lesson.Common;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.LessonTrackingAggregateRoot;
using CodeGo.Domain.LessonTrackingAggregateRoot.Entities;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Lesson.Command.StartLesson;

public class StartLessonCommandHandler : IRequestHandler<StartLessonCommand, ErrorOr<PracticesResult>>
{
    private readonly ILessonTrackingRepository _lessonTrackingRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly IUserRepository _userRepository;
    private readonly IQuestionRepository _questionRepository;
    private readonly IExerciseRepository _exerciseRepository;

    public StartLessonCommandHandler(
        ILessonTrackingRepository lessonTrackingRepository,
        ICourseRepository courseRepository,
        IUserRepository userRepository,
        IQuestionRepository questionRepository,
        IExerciseRepository exerciseRepository)
    {
        _lessonTrackingRepository = lessonTrackingRepository;
        _courseRepository = courseRepository;
        _userRepository = userRepository;
        _questionRepository = questionRepository;
        _exerciseRepository = exerciseRepository;
    }

    public async Task<ErrorOr<PracticesResult>> Handle(
        StartLessonCommand command,
        CancellationToken cancellationToken)
    {
        var userId = UserId.Create(command.UserId);
        var user = await _userRepository.FindById(userId);
        if (user is null)
            return Errors.Users.NotFound;
        var courseId = CourseId.Create(command.CourseId);
        var course = await _courseRepository.FindById(courseId);
        if (course is null)
            return Errors.Course.CourseNotFound;
        var moduleId = ModuleId.Create(command.ModuleId);
        if (!course.HasModule(moduleId))
            return Errors.Course.ModuleNotFound;
        var courseQuestions = await _questionRepository.FindByCourseId(courseId);
        var courseExercises = await _exerciseRepository.FindByCourseId(courseId);

        var moduleQuestions = course.SelectModuleQuestions(courseQuestions, moduleId);
        var moduleExercises = course.SelectModuleExercises(courseExercises, moduleId);

        var practices = moduleExercises
        .ConvertAll(exercise => Practice.CreateNew(
            exercise.Id.Value.ToString(),
            PracticeType.Exercise))
        .Concat(moduleQuestions
            .ConvertAll(question => Practice.CreateNew(
                question.Id.Value.ToString(),
                PracticeType.Question)))
        .ToList();

        var lessonTracking = LessonTracking.CreateNew(
            userId,
            courseId,
            practices
        );

        await _lessonTrackingRepository.AddAsync(lessonTracking);

        return new PracticesResult(
            moduleQuestions,
            moduleExercises);
    }
}
