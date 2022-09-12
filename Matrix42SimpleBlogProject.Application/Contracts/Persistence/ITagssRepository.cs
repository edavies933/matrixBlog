using Matrix42SimpleBlogProject.Domain.Entities;

namespace Matrix42SimpleBlogProject.Application.Contracts.Persistence
{
    public interface ITagsRepository : IAsyncRepository<Tag>
    {
        Task<Tag?> GetTag(Guid tagId);
    }
}