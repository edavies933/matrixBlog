using Matrix42SimpleBlogProject.Domain.Common;

namespace Matrix42SimpleBlogProject.Domain.Entities
{
    public class BlogPost : AuditableEntity
    {
        public Guid BlogId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }


    }
}