using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostDetails
{
    public class GetBlogPostDetailQuery : IRequest<BlogPostDetailVm>
    {
        public Guid BlogPostId { get; set; }
    }
}

