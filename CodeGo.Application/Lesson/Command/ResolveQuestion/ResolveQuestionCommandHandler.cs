
using System.Net;
using System.Security.Cryptography.X509Certificates;
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Lesson.Common;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Lesson.Command.ResolveQuestion;

public class ResolveQuestionCommandHandler : IRequestHandler<ResolveQuestionCommand, ErrorOr<ResolvePracticeResult>>
{
    private readonly ILessonTrackingRepository _lessonTrackingRepository;
    private readonly IUserRepository _userRepository;
    private readonly IQuestionRepository _questionRepository;

    public ResolveQuestionCommandHandler(
        ILessonTrackingRepository lessonTrackingRepository,
        IQuestionRepository questionRepository,
        IUserRepository userRepository)
    {
        _lessonTrackingRepository = lessonTrackingRepository;
        _questionRepository = questionRepository;
        _userRepository = userRepository;
    }


    public async Task<ErrorOr<ResolvePracticeResult>> Handle(
        ResolveQuestionCommand command,
        CancellationToken cancellationToken)
    {
        var userId = UserId.Create(command.UserId);
        var lessonTracking = await _lessonTrackingRepository.FindByIdAndUserId(
            LessonTrackingId.Create(command.LessonTrackingId),
            userId);
        if (lessonTracking is null)
            return Errors.LessonTrackings.NotFound;
        var question = await _questionRepository.FindById(QuestionId.Create(command.QuestionId));
        if (question is null)
            return Errors.Question.NotFound;
        var questionResult = question.Resolve(AlternativeId.Create(command.AlternativeId), userId);
        if (questionResult.IsError)
            return questionResult.Errors;
        var lessonResult = lessonTracking.ResolvePractice(
            activityId: question.Id.Value.ToString(),
            answerId: command.AlternativeId,
            isCorrect: questionResult.Value,
            difficulty: question.Difficulty,
            userId: userId
        );
        if (lessonResult.IsError)
            return lessonResult.Errors;
        await _lessonTrackingRepository.UpdateAsync(lessonTracking);
        var message = questionResult.Value ? "Resposta Correta" : "Resposta Errada";
        return new ResolvePracticeResult(
            message,
            questionResult.Value);
    }
}
