using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.DeleteBlogPost
{
    public class DeleteBlogPostCommandHandler : IRequestHandler<DeleteBlogPostCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository;
        private readonly ILogger<DeleteBlogPostCommandHandler> logger;


        public DeleteBlogPostCommandHandler(IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository, ILogger<DeleteBlogPostCommandHandler> logger)
        {
            this.blogPostRepository = blogPostRepository;
            this.logger = logger;
        }

        public async Task<Unit> Handle(DeleteBlogPostCommand request, CancellationToken cancellationToken)
        {
            var blogPostToDelete = await blogPostRepository.GetByIdAsync(request.BlogPostId);

            if (blogPostToDelete is null)
            {
                throw new NotFoundException(nameof(BlogPost), request.BlogPostId);
            }

            await blogPostRepository.DeleteAsync(blogPostToDelete);
            logger.LogInformation($" blog post with id {blogPostToDelete.Id} deleted");

            return Unit.Value;

        }
    }
}
