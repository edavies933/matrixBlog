using Matrix42SimpleBlogProject.Domain.Common;

namespace Matrix42SimpleBlogProject.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
    }
}