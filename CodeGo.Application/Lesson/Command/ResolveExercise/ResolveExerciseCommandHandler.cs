
using CodeGo.Application.Common.Interfaces.Http;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Lesson.Common;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.ExerciseAggregateRoot.ValueObjects;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Lesson.Command.ResolveExercise;

public class ResolveExerciseCommandHandler : IRequestHandler<ResolveExerciseCommand, ErrorOr<ResolvePracticeResult>>
{
    private readonly ILessonTrackingRepository _lessonTrackingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IExerciseRepository _exerciseRepository;
    private readonly ICourseRepository _courseRepository;
    private readonly ICompilerApi _compilerApi;

    public ResolveExerciseCommandHandler(
        ILessonTrackingRepository lessonTrackingRepository,
        IUserRepository userRepository,
        IExerciseRepository exerciseRepository,
        ICourseRepository courseRepository,
        ICompilerApi compilerApi)
    {
        _lessonTrackingRepository = lessonTrackingRepository;
        _userRepository = userRepository;
        _exerciseRepository = exerciseRepository;
        _courseRepository = courseRepository;
        _compilerApi = compilerApi;
    }

    public async Task<ErrorOr<ResolvePracticeResult>> Handle(ResolveExerciseCommand command, CancellationToken cancellationToken)
    {
        var userId = UserId.Create(command.UserId);
        var lessonTracking = await _lessonTrackingRepository.FindByIdAndUserId(
            LessonTrackingId.Create(command.LessonTrackingId),
            userId);
        var user = await _userRepository.FindById(userId);
        if (user is null)
            return Errors.Users.NotFound;
        if (lessonTracking is null)
            return Errors.LessonTrackings.NotFound;
        var exerciseId = ExerciseId.Create(command.ExerciseId);
        var exercise = await _exerciseRepository.FindById(exerciseId);
        if (exercise is null)
            return Errors.Exercise.NotFound;
        var testCaseId = TestCaseId.Create(command.TestCaseId);
        var runCode = exercise.MakeRunCode(command.SolutionCode);
        var course = await _courseRepository.FindById(exercise.CourseId);
        if (course is null)
            return Errors.Course.CourseNotFound;
        // Make validation when the code return an exception from compiler
        var result = await _compilerApi.SendCodeToCompile(runCode, course.Language);
        var exerciseResult = exercise.Resolve(
            userId,
            testCaseId,
            result);
        if (exerciseResult.IsError)
            return exerciseResult.Errors;
        var lessonResult = lessonTracking.ResolvePractice(
            activityId: exercise.Id.Value.ToString(),
            answerId: command.TestCaseId,
            isCorrect: exerciseResult.Value,
            difficulty: exercise.Difficulty,
            user: user);
        if (lessonResult.IsError)
            return lessonResult.Errors;
        await _lessonTrackingRepository.UpdateAsync(lessonTracking);
        var message = exerciseResult.Value ? "Sucesso no Teste" : "Falha no Teste";
        return new ResolvePracticeResult(
            message,
            exerciseResult.Value,
            lessonResult.Value,
            lessonResult.Value ? user.Life.Count + 1 : user.Life.Count - 1);
    }
}
