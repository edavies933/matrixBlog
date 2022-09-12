using MediatR;

namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Commands.CreateBlogPost
{
    public class CreateBlogPostCommand : IRequest<Guid>
    {
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid BlogId { get; set; }
        public Guid UserId { get; set; }
        public  List<Guid>? TagIds { get; set; }
    }

}
