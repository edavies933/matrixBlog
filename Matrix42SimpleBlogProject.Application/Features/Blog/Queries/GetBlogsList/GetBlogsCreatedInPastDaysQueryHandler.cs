using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogsList;

public class GetBlogsCreatedInPastDaysQueryHandler : IRequestHandler<GetBlogsCreatedInPastDaysQuery, List<BlogListVm>>
{
    private readonly IAsyncRepository<Domain.Entities.Blog> blogRepository;
    private readonly IMapper mapper;

    public GetBlogsCreatedInPastDaysQueryHandler(IMapper mapper, IAsyncRepository<Domain.Entities.Blog> blogRepository)
    {
        this.mapper = mapper;
        this.blogRepository = blogRepository;
    }

    public async Task<List<BlogListVm>> Handle(GetBlogsCreatedInPastDaysQuery request, CancellationToken cancellationToken)
    {
        var pastDate = DateTime.Today.AddDays(-request.DaysAgo);
        var allBlogs = (await blogRepository.ListAllAsync()).OrderBy(x => x.CreatedDate).Where(x => x.CreatedDate > pastDate);
        return mapper.Map<List<BlogListVm>>(allBlogs);
    }
}