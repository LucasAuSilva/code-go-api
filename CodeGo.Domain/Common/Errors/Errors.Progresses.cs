
using ErrorOr;

namespace CodeGo.Domain.Common.Errors;

public static partial class Errors
{
    public static class Progresses
    {
        public static Error NotFoundByUserIdAndCourseId => Error.NotFound(
            code: "Progress.NotFound",
            description: "Progress with this UserId and CourseId doesn't exists");
        
        public static Error NotFoundModuleTrackingWithCurrent => Error.NotFound(
            code: "Progress.ModuleTracking.NotFoundWithCurrent",
            description: "ModuleTracking with Status Current was not found");
    }
}
