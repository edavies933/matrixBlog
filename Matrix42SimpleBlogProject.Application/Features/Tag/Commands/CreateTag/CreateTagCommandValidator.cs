using FluentValidation;

namespace Matrix42SimpleBlogProject.Application.Features.Tag.Commands.CreateTag;

public class CreateTagCommandValidator : AbstractValidator<CreateTagCommand>
{
    public CreateTagCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().WithMessage("{PropertyName} is required.");
    }
}