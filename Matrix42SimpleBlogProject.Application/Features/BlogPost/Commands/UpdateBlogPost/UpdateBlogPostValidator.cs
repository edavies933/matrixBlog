using FluentValidation;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.UpdateBlogPost
{
    public class UpdateBlogPostValidator : AbstractValidator<UpdateBlogPostCommand>
    {
        public UpdateBlogPostValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Content).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
