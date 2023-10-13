
using ErrorOr;

namespace CodeGo.Domain.Common.Errors;

public static partial class Errors
{
    public static class Course
    {
        public static Error LanguageNotFound => Error.NotFound(
            code: "Course.LanguageNotFound",
            description: "Language with this value doesn't exists");

        public static Error CourseNotFound => Error.NotFound(
            code: "Course.NotFound",
            description: "Course with this id doesn't exists");

        public static Error ModuleNotFound => Error.NotFound(
            code: "Course.ModuleNotFound",
            description: "Module with this id doesn't exists");

        public static Error ModuleWithPositionAlreadyExists => Error.NotFound(
            code: "Course.Module.PositionAlreadyExists",
            description: "Module with this Position already exists");

        public static Error SectionWithPositionAlreadyExists => Error.NotFound(
            code: "Course.Section.PositionAlreadyExists",
            description: "Section with this Position already exists");
    }
}
