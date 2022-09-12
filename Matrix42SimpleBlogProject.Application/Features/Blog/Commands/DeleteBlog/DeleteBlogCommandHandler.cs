using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.DeleteBlog;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Matrix42SimpleBlogProject.Application.Features.Blog.Commands.DeleteBlog
{
    public class DeleteBlogCommandHandler : IRequestHandler<DeleteBlogCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.Blog> blogRepository;
        private readonly ILogger<DeleteBlogCommandHandler> logger;


        public DeleteBlogCommandHandler(IAsyncRepository<Domain.Entities.Blog> blogRepository, ILogger<DeleteBlogCommandHandler> logger)
        {
            this.blogRepository = blogRepository;
            this.logger = logger;
        }

        public async Task<Unit> Handle(DeleteBlogCommand request, CancellationToken cancellationToken)
        {
            var blogToDelete = await blogRepository.GetByIdAsync(request.Id);

            if (blogToDelete is null)
            {
                throw new NotFoundException(nameof(Blog), request.Id);
            }

            await blogRepository.DeleteAsync(blogToDelete);
            logger.LogInformation($"Blog  with id {blogToDelete.Id} deleted");

            return Unit.Value;

        }
    }
}
