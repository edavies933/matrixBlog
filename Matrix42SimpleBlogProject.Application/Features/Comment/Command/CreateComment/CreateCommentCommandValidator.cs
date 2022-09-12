using FluentValidation;

namespace Matrix42SimpleBlogProject.Application.Features.Comment.Command.CreateComment
{
    public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
    {
        public CreateCommentCommandValidator()
        {
            RuleFor(c => c.Content).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.BlogPostId).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.UserId).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
