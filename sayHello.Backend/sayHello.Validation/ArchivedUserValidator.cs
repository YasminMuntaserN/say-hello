using sayHello.Entities;

namespace sayHello.Validation;

using FluentValidation;

public class ArchivedUserValidator : AbstractValidator<ArchivedUser>
{
    public ArchivedUserValidator()
    {
        RuleFor(archivedUser => archivedUser.UserId)
            .GreaterThan(0).WithMessage("User ID must be a positive integer.");

        RuleFor(archivedUser => archivedUser.ArchivedUserId)
            .GreaterThan(0).WithMessage("ArchivedUser ID must be a positive integer.");

        RuleFor(archivedUser => archivedUser.DateArchived)
            .NotEmpty().WithMessage("Date Archived is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date Archived cannot be in the future.");
    }
}

