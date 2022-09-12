using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using Matrix42SimpleBlogProject.Application.Features.Comment.Queries.GetAllComments;
using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostDetails
{
    public class GetBlogPostDetailHandler : IRequestHandler<GetBlogPostDetailQuery, BlogPostDetailVm>
    {
        private readonly IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository;
        private readonly ICommentsRepository commentsRepository;
        private readonly IAsyncRepository<Domain.Entities.BlogPostTag> blogPostTagRepository;
        private readonly ITagsRepository tagsRepository;
        private readonly IMapper mapper;

        public GetBlogPostDetailHandler(IMapper mapper, IAsyncRepository<Domain.Entities.BlogPost> postRepository, ICommentsRepository commentsRepository, ITagsRepository tagsRepository, IAsyncRepository<Domain.Entities.BlogPostTag> blogPostTagRepository)
        {
            this.mapper = mapper;
            this.blogPostRepository = postRepository;
            this.commentsRepository = commentsRepository;
            this.tagsRepository = tagsRepository;
            this.blogPostTagRepository = blogPostTagRepository;
        }

        public async Task<BlogPostDetailVm> Handle(GetBlogPostDetailQuery request, CancellationToken cancellationToken)
        {
            var blogPost = await blogPostRepository.GetByIdAsync(request.BlogPostId);
            if (blogPost is null)
            {
                throw new NotFoundException(nameof(blogPost), request.BlogPostId);
            }
            var blogDetailVm = mapper.Map<BlogPostDetailVm>(blogPost);

            var comments = await commentsRepository.GetAllCommentsForBlogPostWithId(request.BlogPostId);
            blogDetailVm.Comments = mapper.Map<List<CommentListVm>>(comments);
            var blogPostTags = (await blogPostTagRepository.ListAllAsync()).Where(t => t.BlogPostId.Equals(request.BlogPostId)).ToList();

            if (!blogPostTags.Any()) return blogDetailVm;

            var postTags = new List<string>();
            foreach (var tag in blogPostTags)
            {
                var tagName = await tagsRepository.GetTag(tag.TagId);
                if (tagName != null) postTags.Add(tagName.Name);
            }
            blogDetailVm.Tags = postTags;
            return blogDetailVm;
        }
    }
}
