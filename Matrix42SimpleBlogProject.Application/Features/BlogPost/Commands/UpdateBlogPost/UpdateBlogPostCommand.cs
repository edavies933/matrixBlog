using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.UpdateBlogPost
{
    public class UpdateBlogPostCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }

}