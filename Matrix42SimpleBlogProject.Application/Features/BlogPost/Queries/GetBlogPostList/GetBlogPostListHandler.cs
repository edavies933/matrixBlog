using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostList
{
    public class GetBlogPostListHandler : IRequestHandler<GetBlogPostListQuery, List<BlogPostListVm>>
    {
        private readonly IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository;
        private readonly IAsyncRepository<Domain.Entities.Blog> blogRepository;

        private readonly IMapper mapper;

        public GetBlogPostListHandler(IMapper mapper, IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository, IAsyncRepository<Domain.Entities.Blog> blogRepository)
        {
            this.mapper = mapper;
            this.blogRepository = blogRepository;
            this.blogPostRepository = blogPostRepository;
        }


        public async Task<List<BlogPostListVm>> Handle(GetBlogPostListQuery request, CancellationToken cancellationToken)
        {
            var blog = await blogRepository.GetByIdAsync(request.BlogId);

            if (blog is null)
            {
                throw new NotFoundException(nameof(Blog), request.BlogId);
            }

            var allBlogPosts = (await blogPostRepository.ListAllAsync()).Where(b=>b.BlogId.Equals(request.BlogId));

            return mapper.Map<List<BlogPostListVm>>(allBlogPosts);
        }
    }
}
