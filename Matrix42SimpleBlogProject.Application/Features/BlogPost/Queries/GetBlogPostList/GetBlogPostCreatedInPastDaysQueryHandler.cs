using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostList;

public class GetPostCreatedInPastDaysQueryHandler : IRequestHandler<GetBlogPostCreatedInPastDaysQuery, List<BlogPostListVm>>
{
    private readonly IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository;
    private readonly IMapper mapper;

    public GetPostCreatedInPastDaysQueryHandler(IMapper mapper, IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository)
    {
        this.mapper = mapper;
        this.blogPostRepository = blogPostRepository;
    }


    public async Task<List<BlogPostListVm>> Handle(GetBlogPostCreatedInPastDaysQuery request, CancellationToken cancellationToken)
    {
        var pastDate = DateTime.Today.AddDays(-request.DaysAgo);
        var allBlogs = (await blogPostRepository.ListAllAsync()).OrderBy(x => x.CreatedDate).Where(x => x.CreatedDate > pastDate);
        return mapper.Map<List<BlogPostListVm>>(allBlogs);
    }
}