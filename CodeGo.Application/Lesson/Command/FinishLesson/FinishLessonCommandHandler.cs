
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Application.Lesson.Common;
using CodeGo.Domain.Common.Errors;
using CodeGo.Domain.LessonTrackingAggregateRoot.ValueObjects;
using CodeGo.Domain.UserAggregateRoot.ValueObjects;
using ErrorOr;
using MediatR;

namespace CodeGo.Application.Lesson.Command.FinishLesson;

public class FinishLessonCommandHandler : IRequestHandler<FinishLessonCommand, ErrorOr<FinishLessonResult>>
{
    private readonly ILessonTrackingRepository _lessonTrackingRepository;

    public FinishLessonCommandHandler(ILessonTrackingRepository lessonTrackingRepository)
    {
        _lessonTrackingRepository = lessonTrackingRepository;
    }

    // TODO: Maybe return the user ranking points
    public async Task<ErrorOr<FinishLessonResult>> Handle(
        FinishLessonCommand command,
        CancellationToken cancellationToken)
    {
        var lesson = await _lessonTrackingRepository.FindByIdAndUserId(
            LessonTrackingId.Create(command.LessonTrackingId),
            UserId.Create(command.UserId));
        if (lesson is null)
            return Errors.LessonTrackings.NotFound;
        var result = lesson.Finish();
        if (result.IsError)
            return result.Errors;
        await _lessonTrackingRepository.UpdateAsync(lesson);
        var message = result.Value
            ? "Parabéns você finalizou essa lição com sucesso" 
            : "Que pena você falhou na lição, continue tentando";
        return new FinishLessonResult(
            message,
            !result.Value);
    }
}
