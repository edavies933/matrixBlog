using Matrix42SimpleBlogProject.Domain.Entities;

namespace Matrix42SimpleBlogProject.Application.Contracts.Persistence
{
    public interface ICommentsRepository : IAsyncRepository<Comment>
    {
        Task<List<Comment>> GetAllCommentsForBlogPostWithId(Guid blogPostId);
    }
}