
using ErrorOr;

namespace CodeGo.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Validation(
            code: "User.DuplicateEmail",
            description: "User with this email already exists");

        public static Error NotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User with this id doesn't exists");

        public static Error RequesterNotFound => Error.NotFound(
            code: "User.RequesterNotFound",
            description: "Requester with this id doesn't exists");

        public static Error RequestNotFound => Error.NotFound(
            code: "User.FriendshipRequestNotFound",
            description: "Request with this id doesn't exists");

        public static Error CantAccess => Error.Custom(
            type: CustomErrorTypes.Forbidden,
            code: "User.CantAccess",
            description: "User logged can access this content");

        public static Error ProfileVisibilityIncorrect => Error.Validation(
            code: "User.ProfileVisibilityIncorrect",
            description: "Profile Visibility value is not valid");
    }
}
