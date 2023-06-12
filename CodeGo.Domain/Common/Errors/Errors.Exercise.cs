
using ErrorOr;

namespace CodeGo.Domain.Common.Errors;

public static partial class Errors
{
    public static class Exercise
    {
        public static Error NotFound => Error.Validation(
            code: "Exercise.NotFound",
            description: "Exercise with this id doesn't exists");

        public static Error TestCaseNotFound => Error.Validation(
            code: "TestCase.NotFound",
            description: "TestCase with this id doesn't exists");
    }
}
