using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogsList
{
    public class GetBlogsListQuery : IRequest<List<BlogListVm>>
    {
    }
}

