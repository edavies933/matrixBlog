using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Comment.Command.CreateComment
{
    public class CreateCommentCommand : IRequest<Guid>
    {
        public string Content { get; set; } = string.Empty;
        public Guid BlogPostId { get; set; }
        public Guid UserId { get; set; }
    }
}
