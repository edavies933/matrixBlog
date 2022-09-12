using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostList
{
    public class GetBlogPostListQuery : IRequest<List<BlogPostListVm>>
    {
        public Guid BlogId { get; set; }
    }
}

