
using ErrorOr;

namespace CodeGo.Domain.Common.Errors;

public static partial class Errors
{
    public static class LessonTrackings
    {
        public static Error NotFound => Error.Validation(
            code: "LessonTracking.NotFound",
            description: "LessonTracking with this id was not found");

        public static Error PracticeNotFoundByActivity => Error.Validation(
            code: "LessonTracking.Practice.NotFoundByActivityId",
            description: "Practice with this ActivityId was not found");

        public static Error PracticeAlreadyAnswered => Error.Validation(
            code: "LessonTracking.Practice.AlreadyAnswered",
            description: "Practice can only have one answer");
    }
}
