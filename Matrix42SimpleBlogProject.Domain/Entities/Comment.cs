using Matrix42SimpleBlogProject.Domain.Common;

namespace Matrix42SimpleBlogProject.Domain.Entities
{
    public class Comment : AuditableEntity
    {
        public Guid BlogPostId { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid UserId { get; set; }

    }
}