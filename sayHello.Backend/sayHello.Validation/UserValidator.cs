using sayHello.Entities;

namespace sayHello.Validation;

using FluentValidation;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(user => user.Username)
            .NotEmpty().WithMessage("Username is required.")
            .Length(3, 50).WithMessage("Username must be between 3 and 50 characters.");

        RuleFor(user => user.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .Length(5, 100).WithMessage("Email must be between 5 and 100 characters.");

        RuleFor(user => user.Password)
            .NotEmpty().WithMessage("Password is required.")
            .Length(8, 255).WithMessage("Password must be at least 8 characters long.");

        RuleFor(user => user.ProfilePictureUrl)
            .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _)).When(user => !string.IsNullOrEmpty(user.ProfilePictureUrl))
            .WithMessage("Invalid URL format for Profile Picture.");

        RuleFor(user => user.Bio)
            .MaximumLength(500).WithMessage("Bio cannot exceed 500 characters.");

        RuleFor(user => user.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(status => new[] { "Online", "Offline", "Last Seen" }.Contains(status))
            .WithMessage("Status must be 'Online', 'Offline', or 'Last Seen'.");

        RuleFor(user => user.DateJoined)
            .NotEmpty().WithMessage("Date Joined is required.")
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Date Joined cannot be in the future.");

        RuleFor(user => user.LastLogin)
            .LessThanOrEqualTo(DateTime.Now).When(user => user.LastLogin.HasValue)
            .WithMessage("Last Login cannot be in the future.");
    }
}
