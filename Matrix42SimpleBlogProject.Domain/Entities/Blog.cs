using Matrix42SimpleBlogProject.Domain.Common;

namespace Matrix42SimpleBlogProject.Domain.Entities
{
    public class Blog : AuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}