using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Users.Command.ResponseFriendshipRequest;

public class ResponseFriendshipRequestCommandValidator : AbstractValidator<ResponseFriendshipRequestCommand>
{
    public ResponseFriendshipRequestCommandValidator()
    {
        RuleFor(x => x.LoggedUserId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId()
            .Equal(x => x.LoggedUserId);
        RuleFor(x => x.RequestId)
            .NotEmpty()
            .IsId();
        // Can be: Accepted - 2, Refused - 3, Ignored - 4, Blocked - 5
        RuleFor(x => x.Response)
            .NotEmpty()
            .GreaterThan(1)
            .LessThanOrEqualTo(5);
    }
}
