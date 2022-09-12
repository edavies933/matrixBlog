using FluentValidation;

namespace Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.UpdateBlog
{
    public class UpdateBlogCommandValidator : AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Description).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
