using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogsList;

public class GetBlogsCreatedInPastDaysQuery : IRequest<List<BlogListVm>>
{
    public int DaysAgo { set; get; }
}