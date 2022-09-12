using AutoMapper;
using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Matrix42SimpleBlogProject.Application.Features.Tag.Commands.CreateTag;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Guid>
{
    private readonly IAsyncRepository<Domain.Entities.Tag> tagRepository;
    private readonly IMapper mapper;
    private readonly ILogger<CreateTagCommandHandler> logger;
    public CreateTagCommandHandler(IAsyncRepository<Domain.Entities.Tag> tagRepository, IMapper mapper, ILogger<CreateTagCommandHandler> logger)
    {
        this.tagRepository = tagRepository;
        this.mapper = mapper;
        this.logger = logger;
    }

    public async Task<Guid> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateTagCommandValidator();
        var validationResult = validator.Validate(request);

        if (validationResult.Errors.Count > 0)
            throw new Exceptions.ValidationException(validationResult);
        var tag = mapper.Map<Domain.Entities.Tag>(request);
        tag.Id = Guid.NewGuid();
        tag = await tagRepository.AddAsync(tag);

        logger.LogInformation($"New Tag with id {tag.Id} created");
        return tag.Id;

    }
}