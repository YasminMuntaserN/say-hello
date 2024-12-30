using sayHello.Entities;

namespace sayHello.Validation;

using FluentValidation;

public class GroupValidator : AbstractValidator<Group>
{
    public GroupValidator()
    {

        RuleFor(Group => Group.CreatedAt)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Send Date/Time cannot be in the future.");

        RuleFor(Group => Group.Name)
        .NotEmpty().WithMessage("Group name is required.")
        .Length(3, 50).WithMessage("Group name must be between 3 and 50 characters.");

    }
}

