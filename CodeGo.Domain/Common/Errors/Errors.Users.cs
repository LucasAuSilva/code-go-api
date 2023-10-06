
using System.Net.NetworkInformation;
using ErrorOr;

namespace CodeGo.Domain.Common.Errors;

public static partial class Errors
{
    public static class Users
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

        public static Error UserRoleIncorrect => Error.Validation(
            code: "User.UserRoleIncorrect",
            description: "This Role value is not valid");

        public static Error FriendRequestStatusIncorrect => Error.Validation(
            code: "User.FriendRequestStatusIncorrect",
            description: "Friend Status value is not valid");

        public static Error Blocked => Error.Validation(
            code: "User.Blocked",
            description: "This requester was blocked by the user");

        public static Error AlreadyRequested => Error.Validation(
            code: "User.AlreadyRequested",
            description: "Already send an friend request to this user");
    }
}
