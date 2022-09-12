using Matrix42SimpleBlogProject.Domain.Common;

namespace Matrix42SimpleBlogProject.Domain.Entities;

public class BlogPostTag : BaseEntity
{
    public Guid BlogPostId { get; set; }
    public Guid TagId { get; set; }
}