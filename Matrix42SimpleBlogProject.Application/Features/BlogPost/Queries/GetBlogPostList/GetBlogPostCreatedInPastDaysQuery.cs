using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostList;

public class GetBlogPostCreatedInPastDaysQuery : IRequest<List<BlogPostListVm>>
{
    public int DaysAgo { set; get; }
}