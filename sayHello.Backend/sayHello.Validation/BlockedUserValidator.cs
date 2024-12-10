using sayHello.Entities;

namespace sayHello.Validation;

using FluentValidation;

public class BlockedUserValidator : AbstractValidator<BlockedUser>
{
    public BlockedUserValidator()
    {
        RuleFor(blockedUser => blockedUser.UserId)
            .GreaterThan(0).WithMessage("User ID must be a positive integer.");

        RuleFor(blockedUser => blockedUser.BlockedByUserId)
            .GreaterThan(0).WithMessage("BlockedByUser ID must be a positive integer.");

        RuleFor(blockedUser => blockedUser.DateBlocked)
            .NotEmpty().WithMessage("Date Blocked is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date Blocked cannot be in the future.");

        RuleFor(blockedUser => blockedUser.Reason)
            .MaximumLength(500).WithMessage("Reason cannot exceed 500 characters.")
            .When(blockedUser => !string.IsNullOrEmpty(blockedUser.Reason));
    }
}
