
namespace Matrix42SimpleBlogProject.Domain.Common
{
   public class AuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
