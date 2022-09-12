using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Tag.Commands.DeleteTag
{
    public class DeleteTagCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
