using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Matrix42SimpleBlogProject.Application.Features.Comment.Command.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.Comment> commentRepository;
        private readonly ILogger<DeleteCommentCommandHandler> logger;


        public DeleteCommentCommandHandler(IAsyncRepository<Domain.Entities.Comment> commentRepository, ILogger<DeleteCommentCommandHandler> logger)
        {
            this.commentRepository = commentRepository;
            this.logger = logger;
        }


        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var commentToBeDeleted = await commentRepository.GetByIdAsync(request.Id);

            if (commentToBeDeleted is null)
            {
                throw new NotFoundException(nameof(BlogPost), request.Id);
            }

            await commentRepository.DeleteAsync(commentToBeDeleted);
            logger.LogInformation($"Comment with id {commentToBeDeleted.Id} deleted");

            return Unit.Value;
        }
    }
}

