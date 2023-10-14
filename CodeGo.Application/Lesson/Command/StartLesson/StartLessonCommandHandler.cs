
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Lesson.Common;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.LessonTrackingAggregateRoot;
using CodeGo.Domain.LessonTrackingAggregateRoot.Entities;
using CodeGo.Domain.LessonTrackingAggregateRoot.Enums;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
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
    private readonly IProgressRepository _progressRepository;

    public StartLessonCommandHandler(
        ILessonTrackingRepository lessonTrackingRepository,
        ICourseRepository courseRepository,
        IUserRepository userRepository,
        IQuestionRepository questionRepository,
        IExerciseRepository exerciseRepository,
        IProgressRepository progressRepository)
    {
        _lessonTrackingRepository = lessonTrackingRepository;
        _courseRepository = courseRepository;
        _userRepository = userRepository;
        _questionRepository = questionRepository;
        _exerciseRepository = exerciseRepository;
        _progressRepository = progressRepository;
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
        var progress = await _progressRepository.FindByUserIdAndCourseId(userId, courseId);
        if (progress is null)
            return Errors.Progresses.NotFoundByUserIdAndCourseId;
        var moduleId = ModuleId.Create(command.ModuleId);
        if (!course.HasModule(moduleId))
            return Errors.Course.ModuleNotFound;
        var courseQuestions = await _questionRepository.FindByCourseId(courseId);
        var courseExercises = await _exerciseRepository.FindByCourseId(courseId);

        var moduleQuestions = course.SelectModuleQuestions(courseQuestions, moduleId);
        var moduleExercises = course.SelectModuleExercises(courseExercises, moduleId);

        // TODO: Make check to see if requested module is the current or not
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

        progress.AddLessonTrackingId(LessonTrackingId.Create(lessonTracking.Id.Value));
        await _lessonTrackingRepository.AddAsync(lessonTracking);

        return new PracticesResult(
            lessonTracking.IdToValueObject(),
            moduleQuestions,
            moduleExercises);
    }
}
