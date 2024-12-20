using sayHello.Entities;

namespace sayHello.Validation;

using FluentValidation;

public class MessageValidator : AbstractValidator<Message>
{
    public MessageValidator()
    {
        RuleFor(message => message.Content)
            .NotEmpty().WithMessage("Message content is required.")
            .MaximumLength(1000).WithMessage("Message content cannot exceed 1000 characters.");

        RuleFor(message => message.SendDT)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("Send Date/Time cannot be in the future.");

        RuleFor(message => message.ReadDT)
            .GreaterThanOrEqualTo(message => message.SendDT)
            .When(message => message.ReadDT.HasValue)
            .WithMessage("Read Date/Time must be after Send Date/Time.");

        RuleFor(message => message.ReadStatus)
            .NotEmpty().WithMessage("Read status is required.")
            .Must(status => new[] { "Read", "UnRead" }.Contains(status))
            .WithMessage("Read status must be 'Read' or 'UnRead'.");

        RuleFor(message => message.SenderId)
            .GreaterThan(0).WithMessage("Sender Id must be a positive integer.");

        RuleFor(message => message.ReceiverId)
            .GreaterThanOrEqualTo(0).WithMessage("Receiver Id must be a non-negative integer or null.");

    }
}

