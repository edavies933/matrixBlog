using MediatR;
namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.DeleteBlogPost
{
    public class DeleteBlogPostCommand : IRequest
    {
        public Guid BlogPostId { get; set; }
    }
}
