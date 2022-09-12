using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Comment.Command.DeleteComment
{
    public class DeleteCommentCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}

