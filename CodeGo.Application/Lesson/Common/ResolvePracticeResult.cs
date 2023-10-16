
namespace CodeGo.Application.Lesson.Common;

public record ResolvePracticeResult(
    string Message,
    bool IsCorrect,
    bool IsLessonFailed,
    int UserLifeCount
);
