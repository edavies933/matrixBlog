using FluentValidation;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.CreateBlogPost
{
    public class CreateBlogPostValidator : AbstractValidator<CreateBlogPostCommand>
    {
        public CreateBlogPostValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Content).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
