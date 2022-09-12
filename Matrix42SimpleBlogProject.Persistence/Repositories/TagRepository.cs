using Matrix42SimpleBlogProject.Application.Contracts.Persistence;
using Matrix42SimpleBlogProject.Domain.Entities;

namespace Matrix42SimpleBlogProject.Persistence.Repositories
{
    public class TagRepository : BaseRepository<Tag>, ITagsRepository
    {

        public TagRepository(BlogProjectDbContext dbContext) : base(dbContext)
        {
        }


        public Task<Tag?> GetTag(Guid blogPostId)
        {
            var blogPostsTag = DbContext.Tags.FirstOrDefault(t=>t.Id.Equals(blogPostId));

            return Task.FromResult(blogPostsTag);
        }
    }
}