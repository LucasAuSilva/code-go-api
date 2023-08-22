
using ErrorOr;

namespace CodeGo.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Validation(
            code: "User.DuplicateEmail",
            description: "User with this email already exists");

        public static Error UserNotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User with this id doesn't exists");
    }
}
