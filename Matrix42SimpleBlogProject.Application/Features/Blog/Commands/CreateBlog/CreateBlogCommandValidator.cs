using FluentValidation;

namespace Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.CreateBlog
{
    public class CreateBlogCommandValidator : AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogCommandValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("{PropertyName} is required.");
            RuleFor(c => c.Description).NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
