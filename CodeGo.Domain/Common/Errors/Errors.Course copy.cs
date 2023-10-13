
using ErrorOr;

namespace CodeGo.Domain.Common.Errors;

public static partial class Errors
{
    public static class Progresses
    {
        public static Error NotFoundByUserIdAndCourseId => Error.NotFound(
            code: "Progress.NotFound",
            description: "Progress with this UserId and CourseId doesn't exists");
    }
}
