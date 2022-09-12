using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogsList
{
    public class GetBlogsListHandler : IRequestHandler<GetBlogsListQuery, List<BlogListVm>>
    {
        private readonly IAsyncRepository<Domain.Entities.Blog> blogRepository;
        private readonly IMapper mapper;

        public GetBlogsListHandler(IMapper mapper, IAsyncRepository<Domain.Entities.Blog> blogRepository)
        {
            this.mapper = mapper;
            this.blogRepository = blogRepository;
        }

        public async Task<List<BlogListVm>> Handle(GetBlogsListQuery request, CancellationToken cancellationToken)
        {
            var allBlogs = (await blogRepository.ListAllAsync()).OrderBy(x => x.CreatedDate);

            return mapper.Map<List<BlogListVm>>(allBlogs);
        }
    }
}
