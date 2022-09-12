using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.Comment.Queries.GetAllComments
{
    public class CommentListQuery : IRequest<List<CommentListVm>>
    {
        public Guid BlogPostId { get; set; }
    }
}
