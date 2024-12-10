using sayHello.Entities;

namespace sayHello.Validation;

using FluentValidation;

public class MediaValidator : AbstractValidator<Media>
{
    public MediaValidator()
    {
        /* RuleFor(media => media.FilePath)
             .NotEmpty().WithMessage("File path is required.")
             .Must(filePath => Uri.IsWellFormedUriString(filePath, UriKind.Absolute))
             .When(media => !string.IsNullOrEmpty(media.FilePath))
             .WithMessage("File path must be a valid URL if provided.");*/

        RuleFor(media => media.MediaType)
            .NotEmpty().WithMessage("Media type is required.")
            .Must(mediaType => new[] { "image", "video", "audio" }.Contains(mediaType.ToLower()))
            .WithMessage("Media type must be 'image', 'video', or 'audio'.");

        RuleFor(media => media.MessageId)
            .GreaterThan(0).WithMessage("Message ID must be a positive integer.");
    }
}