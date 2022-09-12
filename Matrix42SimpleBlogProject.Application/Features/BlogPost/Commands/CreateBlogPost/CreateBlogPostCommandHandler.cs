using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using Matrix42SimpleBlogProject.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.CreateBlogPost
{
    public class CreateBlogPostCommandHandler : IRequestHandler<CreateBlogPostCommand, Guid>
    {
        private readonly IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository;
        private readonly IAsyncRepository<Domain.Entities.Blog> blogRepository;
        private readonly ITagsRepository tagRepository;
        private readonly IAsyncRepository<BlogPostTag> blogPostTagRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateBlogPostCommandHandler> logger;
        public CreateBlogPostCommandHandler(IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository, IMapper mapper, ILogger<CreateBlogPostCommandHandler> logger, IAsyncRepository<Domain.Entities.Blog> blogRepository, IAsyncRepository<BlogPostTag> blogPostTagRepository, ITagsRepository tagRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.blogRepository = blogRepository;
            this.blogPostTagRepository = blogPostTagRepository;
            this.tagRepository = tagRepository;
        }

        public async Task<Guid> Handle(CreateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBlogPostValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var blogToAddBlogPost = await blogRepository.GetByIdAsync(request.BlogId);

            if (blogToAddBlogPost is null)
            {
                throw new NotFoundException(nameof(Blog), request.BlogId);
            }

            var blogPost = mapper.Map<Domain.Entities.BlogPost>(request);
            blogPost.Id = Guid.NewGuid();

            blogPost = await blogPostRepository.AddAsync(blogPost);

            if (request.TagIds is not null && request.TagIds.Any())
            {
                foreach (var tagId in request.TagIds)
                {
                    var isValid = await tagRepository.GetTag(tagId);
                    if (isValid is null)
                    {
                        logger.LogInformation($"Invalid tag with Id  {tagId} ");
                        continue;
                    }
                    var blogPostTag = new BlogPostTag { Id = Guid.NewGuid(), BlogPostId = blogPost.Id, TagId = tagId, };
                    await blogPostTagRepository.AddAsync(blogPostTag);
                }
            }

            logger.LogInformation($"New blog post with id {blogPost.Id} created");
            return blogPost.Id;
        }
    }

}
