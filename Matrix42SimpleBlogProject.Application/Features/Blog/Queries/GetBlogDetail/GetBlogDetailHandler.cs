using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Blog.Queries.GetBlogDetail
{
    public class GetBlogDetailHandler : IRequestHandler<GetBlogDetailQuery, BlogDetailVm>
    {
        private readonly IAsyncRepository<Domain.Entities.Blog> blogRepository;
        private readonly IMapper mapper;

        public GetBlogDetailHandler(IMapper mapper, IAsyncRepository<Domain.Entities.Blog> blogRepository)
        {
            this.mapper = mapper;
            this.blogRepository = blogRepository;
        }

        public async Task<BlogDetailVm> Handle(GetBlogDetailQuery request, CancellationToken cancellationToken)
        {
            var blog = await blogRepository.GetByIdAsync(request.BlogId);
            if (blog is null)
            {
                throw new NotFoundException(nameof(blog), request.BlogId);
            }
            var blogDetailVm = mapper.Map<BlogDetailVm>(blog);
            return blogDetailVm;
        }
    }
}
