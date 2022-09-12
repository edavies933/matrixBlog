using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using Matrix42SimpleBlogProject.Application.Features.Blogs.Commands.UpdateBlog;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Matrix42SimpleBlogProject.Application.Features.Blog.Commands.UpdateBlog
{
    public class UpdateBlogCommandHandler : IRequestHandler<UpdateBlogCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.Blog> blogRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateBlogCommandHandler> logger;
        public UpdateBlogCommandHandler(IAsyncRepository<Domain.Entities.Blog> blogRepository, IMapper mapper, ILogger<UpdateBlogCommandHandler> logger)
        {
            this.blogRepository = blogRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Unit> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBlogCommandValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var blogToUpdate = await blogRepository.GetByIdAsync(request.Id);

            if (blogToUpdate is null)
            {
                throw new NotFoundException(nameof(Blog), request.Id);
            }

            mapper.Map(request, blogToUpdate, typeof(UpdateBlogCommand), typeof(Domain.Entities.Blog));

            await blogRepository.UpdateAsync(blogToUpdate);

            logger.LogInformation($"Blog with id {blogToUpdate.Id} updated");
            return Unit.Value;
        }
    }
}
