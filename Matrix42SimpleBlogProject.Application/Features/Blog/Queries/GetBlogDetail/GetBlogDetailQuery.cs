using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogDetail
{
    public class GetBlogDetailQuery : IRequest<BlogDetailVm>
    {
        public Guid BlogId { get; set; }
    }
}

