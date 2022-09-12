using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Matrix42SimpleBlogProject.Application.Features.Comment.Command.CreateComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, Guid>
    {
        private readonly IAsyncRepository<Domain.Entities.Comment> commentRepository;
        private readonly IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository;
        private readonly IMapper mapper;
        private readonly ILogger<CreateCommentCommandHandler> logger;
        public CreateCommentCommandHandler(IAsyncRepository<Domain.Entities.Comment> commentRepository, IMapper mapper, ILogger<CreateCommentCommandHandler> logger, IAsyncRepository<Domain.Entities.BlogPost> blogPostRepository)
        {
            this.commentRepository = commentRepository;
            this.mapper = mapper;
            this.logger = logger;
            this.blogPostRepository = blogPostRepository;
        }


        public async Task<Guid> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCommentCommandValidator();
            var validationResult = validator.Validate(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var blogPostToAddComment = await blogPostRepository.GetByIdAsync(request.BlogPostId);

            if (blogPostToAddComment is null)
            {
                throw new NotFoundException(nameof(BlogPost), request.BlogPostId);
            }

            var comment = mapper.Map<Domain.Entities.Comment>(request);
            comment.Id = Guid.NewGuid();
            comment = await commentRepository.AddAsync(comment);

            logger.LogInformation($"New Comment with id {comment.Id} created");
            return comment.Id;
        }
    }
}
