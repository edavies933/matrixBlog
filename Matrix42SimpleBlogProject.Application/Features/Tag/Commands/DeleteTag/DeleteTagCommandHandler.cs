using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Application.Exceptions;
using Matrix42SimpleBlogProject.Application.Features.Tag.Commands.CreateTag;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Matrix42SimpleBlogProject.Application.Features.Tag.Commands.DeleteTag
{
    public class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand>
    {
        private readonly IAsyncRepository<Domain.Entities.Tag> tagRepository;
        private readonly ILogger<DeleteTagCommandHandler> logger;
        public DeleteTagCommandHandler(IAsyncRepository<Domain.Entities.Tag> tagRepository,ILogger<DeleteTagCommandHandler> logger)
        {
            this.tagRepository = tagRepository;
            this.logger = logger;
        }

        public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            var tagToDelete = await tagRepository.GetByIdAsync(request.Id);

            if (tagToDelete is null)
            {
                throw new NotFoundException(nameof(Tag), request.Id);
            }

            await tagRepository.DeleteAsync(tagToDelete);
            logger.LogInformation($" Tag  with id {tagToDelete.Id} deleted");

            return Unit.Value;
        }
    }
}
