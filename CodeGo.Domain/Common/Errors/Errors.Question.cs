
using ErrorOr;

namespace CodeGo.Domain.Common.Errors;

public static partial class Errors
{
    public static class Question
    {
        public static Error NotFound => Error.Validation(
            code: "Question.NotFound",
            description: "Question with this id doesn't exists");

        public static Error AlternativeNotFound => Error.Validation(
            code: "Alternative.NotFound",
            description: "Alternative with this id doesn't exists");
    }
}
