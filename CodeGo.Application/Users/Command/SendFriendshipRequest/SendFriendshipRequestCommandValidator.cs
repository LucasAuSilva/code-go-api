
using CodeGo.Application.Common.Validators;
using FluentValidation;

namespace CodeGo.Application.Users.Command.SendFriendshipRequest;

public class SendFriendshipRequestCommandValidator : AbstractValidator<SendFriendshipRequestCommand>
{
    public SendFriendshipRequestCommandValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .IsId();
        RuleFor(x => x.ReceiverId)
            .NotEmpty()
            .IsId();
    }
}
