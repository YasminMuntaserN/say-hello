using sayHello.Entities;

namespace sayHello.Validation;

using FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    private readonly UniqueValidatorService _uniqueValidatorService;

    public UserValidator(UniqueValidatorService uniqueValidatorService)
    {
        _uniqueValidatorService = uniqueValidatorService;

        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.")
            .MustAsync(async (userName, cancellation) => await _uniqueValidatorService.IsUserNameUniqueAsync(userName)).WithMessage("user Name is already in use.");


        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .Length(5, 100).WithMessage("Email must be between 5 and 100 characters.")
            .MustAsync(async (email, cancellation) => await _uniqueValidatorService.IsEmailUniqueAsync(email)).WithMessage("Email is already in use.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(8, 255).WithMessage("Password must be at least 8 characters long.");

        RuleFor(user => user.Bio)
            .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");

        RuleFor(user => user.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(status => new[] { "Online", "Offline", "Last Seen" }.Contains(status))
            .WithMessage("Status must be 'Online', 'Offline', or 'Last Seen'.");

        RuleFor(user => user.DateJoined)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date Joined cannot be in the future.");

        RuleFor(user => user.LastLogin)
            .LessThanOrEqualTo(DateTime.Now).When(user => user.LastLogin.HasValue)
            .WithMessage("Last Login cannot be in the future.");
    }
}
