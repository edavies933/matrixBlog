using Matrix42SimpleBlogProject.Application.Features.Comment.Queries.GetAllComments;
namespace Matrix42SimpleBlogProject.Application.Features.BlogPost.Queries.GetBlogPostDetails
{
    public class BlogPostDetailVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public ICollection<CommentListVm>? Comments { get; set; } 
        public List<string>? Tags { get; set; }

    }
}
