using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Tag.Commands.CreateTag;

public class CreateTagCommand : IRequest<Guid>
{
    public string Name { get; set; } = string.Empty;
}