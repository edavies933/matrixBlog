using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.UpdateBlogPost
{
    public class UpdateBlogPostCommandHandler : IRequestHandler<UpdateBlogPostCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository;
        private readonly IMapper mapper;
        private readonly ILogger<UpdateBlogPostCommandHandler> logger;

        public UpdateBlogPostCommandHandler(IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository, IMapper mapper, ILogger<UpdateBlogPostCommandHandler> logger)
        {
            this.blogPostRepository = blogPostRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public async Task<Unit> Handle(UpdateBlogPostCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBlogPostValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var blogPostToUpdate = await blogPostRepository.GetByIdAsync(request.Id);

            if (blogPostToUpdate is null)
            {
                throw new NotFoundException(nameof(BlogPost), request.Id);
            }
            mapper.Map(request, blogPostToUpdate, typeof(UpdateBlogPostCommand), typeof(Domain.Entities.BlogPost));

            await blogPostRepository.UpdateAsync(blogPostToUpdate);
            logger.LogInformation($" blog post with id {blogPostToUpdate.Id} updated");

            return Unit.Value;

        }
    }

}